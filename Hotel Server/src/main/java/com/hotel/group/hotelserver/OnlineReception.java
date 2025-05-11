package com.hotel.group.hotelserver;

import com.itextpdf.text.DocumentException;
import java.util.Date;
import java.util.List;


public interface OnlineReception {
    
    int login(String username, String password) throws InvalidCredentialsException;
    List<Room> getAvailableRooms(String from, String to) throws BadRequestException;
    List<Reservation> getReservations(int userId);
    int makeReservation(List<String> roomNumbers, String dateFrom, String dateTo, String notes, int userId) throws BadRequestException;
    void modifyReservation(ModifiedReservation modifiedReservation, int userId) throws BadRequestException;
    void cancelReservation(int reservationNumber, int userId) throws BadRequestException;
    byte[] requestReservationConfirmation(int reservationNumber, int userId) throws BadRequestException, DocumentException;
}
