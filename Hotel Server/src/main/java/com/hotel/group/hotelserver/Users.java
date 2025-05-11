package com.hotel.group.hotelserver;

import java.util.Arrays;
import java.util.List;
import java.util.NoSuchElementException;

public class Users {
    private static final List<User> userList = Arrays.asList(
            new User(1, "User1", "Test123"),
            new User(2, "User2", "Test123")
    );

    private Users() {
        throw new UnsupportedOperationException();
    }

    public static User findUser(String username) {
        return userList
                .stream()
                .filter(user -> username.equals(user.getUsername()))
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());
    }

    public static User findUser(int idUser) {
        return userList
                .stream()
                .filter(user -> idUser == user.getId())
                .findFirst()
                .orElseThrow(() -> new NoSuchElementException());
    }

    public static List<User> getUsers() {
        return userList;
    }
}
