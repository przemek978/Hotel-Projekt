package com.hotel.group.hotelserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import java.util.Date;
import java.util.List;

@XmlAccessorType(XmlAccessType.FIELD)
public class ModifiedReservation {
    public int number;
    public String from;
    public String to;
    public List<Room> rooms;
    public int ownersId;
    public String notes;

    public boolean containsPeriod(String from, String to) {
        Date dateFrom = new Date(from);
        Date dateTo = new Date(to);

        return dateFrom.before(dateTo) && dateTo.after(dateFrom);
    }
    public ModifiedReservation(){}
    public ModifiedReservation(int number, String from, String to, List<Room> rooms, int ownersId, String notes) {
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

    public String getFrom() {
        return from;
    }

    void setFrom(String from) {
        this.from = from;
    }

    public String getTo() {
        return to;
    }

    void setTo(String to) {
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
