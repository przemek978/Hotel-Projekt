package com.hotel.group.hotelserver;

import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.util.logging.Level;
import java.util.logging.Logger;

public class User {
    private int id;
    private String username;
    private String password;
    private byte[] salt;

    public User(int id, String username, String password) {
        try {
            this.id = id;
            this.username = username;
            this.password = password;
        } catch (Exception ex) {
            Logger.getLogger(User.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public int getId() {
        return id;
    }

    public String getPassword() {
        return password;
    }
    
    public String getUsername() {
        return username;
    }
}
