package com.hotel.group.hotelserver;

import java.util.Arrays;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.stream.Collectors;

public class Rooms {
    
    private static final List<Room> roomList = Arrays.asList(
            new Room("11", 1, true, 1, 18.9, "Description"),
            new Room("12", 1, true, 1, 21.7, "Description"),
            new Room("13", 1, false, 2, 19.5, "Description"),
            new Room("14", 1, false, 2, 20.6, "Description"),
            new Room("15", 1, false, 2, 21.3, "Description"),
            new Room("16", 1, true, 1, 19.8, "Description"),
            new Room("17", 1, true, 1, 20.2, "Description"),
            new Room("18", 1, false, 3, 22.4, "Description"),
            new Room("21", 2, true, 0, 28.0, "Description"),
            new Room("22", 2, true, 0, 31.6,"Description"),
            new Room("23", 2, true, 0, 25.9, "Description"),
            new Room("24", 2, true, 0, 32,  "Description"),
            new Room("25", 2, true, 0, 35.8,  "Description"),
            new Room("26", 2, true, 0, 28.3,  "Description"),
            new Room("31", 3, false, 4, 33.8, "Description"),
            new Room("32", 3, false, 4, 30.2, "Description"),
            new Room("33", 3, false, 3, 28.4, "Description"),
            new Room("34", 3, false, 2, 27.5, "Description")
            );
    
    public static List<Room> getAvailableRooms(Date from, Date to) {
        Reservations reservations = Reservations.getInstance();
        
        List<Room> bookedRooms = reservations.getReservationsForPeriod(from, to)
                .stream()
                .map(reservation -> reservation.getRooms())
                .flatMap(Collection::stream)
                .collect(Collectors.toList());
        
        return roomList
                .stream()
                .filter(room -> !bookedRooms.contains(room))
                .collect(Collectors.toList());
    }
    
    private Rooms() {
        throw new UnsupportedOperationException();
    }
    
    public static List<Room> getRooms() {
        return roomList;
    }
    
    public static Room findByNumber(String number) {
        return roomList.stream()
                .filter(room -> number.equals(room.getRoomNumber()))
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());
    }
}
