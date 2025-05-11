package com.hotel.group.hotelserver;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.stream.Collectors;

public class Reservations {

    private static Reservations instance;
    private int consecutiveNumbers = 1;
    private final List<Reservation> reservationList = new ArrayList<>();
    
    private Reservations() {
    }
    
    public static synchronized Reservations getInstance() {
        if (Reservations.instance == null)
            Reservations.instance = new Reservations();
        return Reservations.instance;
    }
    
    public Reservation findByNumber(int number) {
        return reservationList
                .stream()
                .filter(reservation -> reservation.getNumber() == number)
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());
    }
    
    public synchronized void remove(Reservation reservation) {
        reservationList.remove(reservation);
    }
    
    public synchronized int create(List<Room> rooms, Date from, Date to, String notes, int userId) throws RoomUnavailableException {
        List<Room> availableRooms = Rooms.getAvailableRooms(from, to);
        int filteredCount = (int) rooms.stream()
                .filter(room -> availableRooms.contains(room))
                .count();
        if (filteredCount != rooms.size()) {
            throw new RoomUnavailableException();
        }
        Reservation reservation = new Reservation(
                consecutiveNumbers,
                from,
                to,
                rooms,
                userId,
                notes);
        
        reservationList.add(reservation);
        consecutiveNumbers++;
        return reservation.getNumber();
    }
    
    public synchronized void modifyReservation(Reservation reservation) throws RoomUnavailableException {
        Reservation sourceReservation = findByNumber(reservation.getNumber());
        System.out.print(reservationList);
        remove(sourceReservation);
        System.out.print(reservationList);
        List<Room> availableRooms = Rooms.getAvailableRooms(reservation.getFrom(), reservation.getTo());
        reservation.setRooms(reservation.getRooms().stream()
            .map(room -> Rooms.findByNumber(room.getRoomNumber()))
            .collect(Collectors.toList()));
        int filteredCount = (int) reservation.getRooms().stream()
                .filter(room -> availableRooms.contains(room))
                .count();
        if (filteredCount != reservation.getRooms().size()) {
            reservationList.add(sourceReservation);
            throw new RoomUnavailableException();
        }
        reservationList.add(reservation);
    }
    
    public List<Reservation> getReservationsForUserId(int userId) {
        return reservationList
                .stream()
                .filter(reservation -> reservation.getOwnersId() == userId)
                .collect(Collectors.toList());
    }
    
    public List<Reservation> getReservationsForPeriod(Date from, Date to) {
        return reservationList
                .stream()
                .filter(reservation -> reservation.containsPeriod(from, to))
                .collect(Collectors.toList());
    }
}
