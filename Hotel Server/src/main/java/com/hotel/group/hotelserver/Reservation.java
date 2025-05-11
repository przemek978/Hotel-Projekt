package com.hotel.group.hotelserver;

import java.util.Date;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;

@XmlAccessorType(XmlAccessType.FIELD)
public class Reservation {
    private int number;
    private Date from;
    private Date to;
    private List<Room> rooms;
    private int ownersId;
    private String notes;
    
    public boolean containsPeriod(Date from, Date to) {
       return this.from.before(to) && this.to.after(from);
    }
    public Reservation(){}
    public Reservation(int number, Date from, Date to, List<Room> rooms, int ownersId, String notes) {
        this.number = number;
        this.from = from;
        this.to = to;
        this.rooms = rooms;
        this.ownersId = ownersId;
        this.notes = notes;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }
    
    public Date getFrom() {
        return from;
    }

    void setFrom(Date from) {
        this.from = from;
    }

    public Date getTo() {
        return to;
    }

    void setTo(Date to) {
        this.to = to;
    }

    public List<Room> getRooms() {
        return rooms;
    }

    void setRooms(List<Room> rooms) {
        this.rooms = rooms;
    }

    public int getOwnersId() {
        return ownersId;
    }

    public void setOwnersId(int ownersId) {
        this.ownersId = ownersId;
    }

    public String getNotes() {
        return notes;
    }

    void setNotes(String notes) {
        this.notes = notes;
    }
}
