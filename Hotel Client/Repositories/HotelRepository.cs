﻿using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Hotel_Client.Models;
//using Hotel_Client.Models.Requests;
using Hotel_Client.OnlineReceptionImplService;
using Hotel_Client.Repositories.Interfaces;

namespace Hotel_Client.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        public async Task<int> Login(string username, string password)
        {
            try
            {
                var client = new OnlineReceptionImplClient();
                var result = await client.loginAsync(username, password.ToString());

                var userId = result.@return;

                return userId;
            }
            catch (FaultException<InvalidCredentialsException> e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyCollection<Room>?> GetRooms(DateTime from, DateTime to)
        {
            try
            {
                var client = new OnlineReceptionImplClient();

                var result = await client.getAvailableRoomsAsync(from.ToString(CultureInfo.InvariantCulture),
                    to.ToString(CultureInfo.InvariantCulture));

                if (result is null)
                {
                    return null;
                }

                var resultInfo = result.@return;
                var resultInfoList = resultInfo.Select(r => new Room
                {
                    RoomNumber = r.roomNumber,
                    FloorNumber = r.floorNumber,
                    HasDoubleBed = r.hasDoubleBed,
                    NumberOfSingleBeds = r.numberOfSingleBeds,
                    RoomSize = r.roomSize,
                    Description = r.description
                }).ToList();

                return resultInfoList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> MakeReservation(List<string> roomNumbers, DateTime dateFrom, DateTime dateTo, string notes,
            int userId)
        {
            try
            {
                var client = GetModifiedClient().Result;
                var response = await client.makeReservationAsync(roomNumbers.ToArray(),
                    dateFrom.ToString(CultureInfo.InvariantCulture),
                    dateTo.ToString(CultureInfo.InvariantCulture), notes, userId);
                var reservationNumber = response.@return;

                if (reservationNumber == 0)
                {
                    throw new Exception("Something went wrong...");
                }

                return reservationNumber;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyCollection<Reservation>?> GetReservations(int userId)
        {
            try
            {
                var client = GetModifiedClient().Result;
                var response = await client.getReservationsAsync(userId);

                if (response.@return == null || response.@return.Length == 0)
                {
                    throw new Exception("No reservations");
                }

                var resultInfo = response.@return.ToList();
                var reservations = resultInfo.Select(r => new Reservation
                {
                    Number = r.number,
                    From = r.from,
                    To = r.to,
                    Rooms = r.rooms.ToList().ConvertAll(x => new Room
                    {
                        RoomNumber = x.roomNumber,
                        FloorNumber = x.floorNumber,
                        HasDoubleBed = x.hasDoubleBed,
                        NumberOfSingleBeds = x.numberOfSingleBeds,
                        RoomSize = x.roomSize,
                        Description = x.description
                    }),
                    OwnersId = r.ownersId,
                    Notes = r.notes
                }).ToList();

                return reservations;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task CancelReservation(int reservationId, int userId)
        {
            try
            {
                var client = GetModifiedClient().Result;
                await client.cancelReservationAsync(reservationId, userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ModifyReservation(Reservation reservation, int userId)
        {
            try
            {
                var client = GetModifiedClient().Result;
                var modifiedReservation = new modifiedReservation
                {
                    number = reservation.Number,
                    from = reservation.From.ToString(CultureInfo.InvariantCulture),
                    to = reservation.To.ToString(CultureInfo.InvariantCulture),
                    rooms = reservation.Rooms.Select(x => new room
                    {
                        roomNumber = x.RoomNumber,
                        floorNumber = x.FloorNumber,
                        hasDoubleBed = x.HasDoubleBed,
                        numberOfSingleBeds = x.NumberOfSingleBeds,
                        roomSize = x.RoomSize,
                        description = x.Description
                    }).ToArray(),
                    ownersId = reservation.OwnersId,
                    notes = reservation.Notes
                };

                await client.modifyReservationAsync(modifiedReservation, userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> GetConfirmationDocument(int reservationId, int userId)
        {
            try
            {
                var client = GetModifiedClient().Result;
                var response = await client.requestReservationConfirmationAsync(reservationId, userId);

                return response.@return;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static Task<OnlineReceptionImplClient> GetModifiedClient()
        {
            var client = new OnlineReceptionImplClient();
            var modifiedClient = new EndpointAddressBuilder(client.Endpoint.Address);
            modifiedClient.Headers.Add(AddressHeader.CreateAddressHeader("Username", "http://hotelserver.group.hotel.com/", App.Username));
            modifiedClient.Headers.Add(AddressHeader.CreateAddressHeader("Password", "http://hotelserver.group.hotel.com/", App.Password));
            client.Endpoint.Address = modifiedClient.ToEndpointAddress();

            return Task.FromResult(client);
        }
    }
}
