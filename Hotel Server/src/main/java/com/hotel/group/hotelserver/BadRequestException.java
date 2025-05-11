package com.hotel.group.hotelserver;

import javax.xml.ws.WebFault;

@WebFault
public class BadRequestException extends Exception {

    public BadRequestException(String message) {
        super(message);
    }
}
