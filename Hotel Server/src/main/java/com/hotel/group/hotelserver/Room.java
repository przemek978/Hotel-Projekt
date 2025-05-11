package com.hotel.group.hotelserver;


import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;

@XmlAccessorType(XmlAccessType.FIELD)
public class Room {
    private String roomNumber;
    private int floorNumber;
    private boolean hasDoubleBed;
    private int numberOfSingleBeds;
    private double roomSize;
    private String description;

     public Room(){}
    public Room(String roomNumber, int floorNumber, boolean hasDoubleBed,
            int numberOfSingleBeds, double roomSize,
            String description) {
        this.roomNumber = roomNumber;
        this.floorNumber = floorNumber;
        this.hasDoubleBed = hasDoubleBed;
        this.numberOfSingleBeds = numberOfSingleBeds;
        this.roomSize = roomSize;
        this.description = description;
    }
    
    public String getRoomNumber() {
        return roomNumber;
    }

    void setRoomNumber(String roomNumber) {
        this.roomNumber = roomNumber;
    }

    public int getFloorNumber() {
        return this.floorNumber;
    }
    
    void setFloorNumber(int floorNumber) {
        this.floorNumber = floorNumber;
    }

    public boolean hasDoubleBed() {
        return hasDoubleBed;
    }
        
    void setHasDoubleBed(boolean hasDoubleBed) {
        this.hasDoubleBed = hasDoubleBed;
    }
    
    public int getNumberOfSingleBeds() {
        return numberOfSingleBeds;
    }

    void setNumberOfSingleBeds(int numberOfSingleBeds) {
        this.numberOfSingleBeds = numberOfSingleBeds;
    }

    public double getRoomSize() {
        return roomSize;
    }

    void setRoomSize(double roomSize) {
        this.roomSize = roomSize;
    }

    public String getDescription() {
        return description;
    }

    void setDescription(String description) {
        this.description = description;
    }  
}
