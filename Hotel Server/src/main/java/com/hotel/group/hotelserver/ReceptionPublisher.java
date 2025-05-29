package com.hotel.group.hotelserver;

import javax.xml.ws.Endpoint;

public class ReceptionPublisher {
    public static void main(String[] args) {
        //Endpoint.publish("http://localhost:8080/Server/OnlineReceptionImplService", new OnlineReceptionImpl());
        Endpoint.publish("http://localhost:8080/Server/OnlineReceptionImplService", new OnlineReceptionImpl());
        System.out.println("Server is published!");
    }
}
