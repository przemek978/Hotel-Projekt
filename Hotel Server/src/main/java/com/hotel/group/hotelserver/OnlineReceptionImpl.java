package com.hotel.group.hotelserver;

import com.itextpdf.text.BadElementException;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import com.itextpdf.text.Font;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.Phrase;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.stream.Collectors;
import javax.jws.WebService;
import javax.xml.ws.soap.MTOM;
import javax.jws.HandlerChain;


@MTOM
@WebService
@HandlerChain(file="handler-chain.xml")
public class OnlineReceptionImpl implements OnlineReception {

    @Override
    public int login(String username, String password) throws InvalidCredentialsException {
        try {
            User user = Users.findUser(username);

            if (user.getPassword().equals(password)) {
                return user.getId();
            }
        } catch (Exception ignore) {
        }
        throw new InvalidCredentialsException("Invalid username or password");
    }

    @Override
    public List<Room> getAvailableRooms(String from, String to) throws BadRequestException {
        Date fromDate = new Date(from);
        Date toDate = new Date(to);

        if (fromDate.after(toDate) || fromDate.before(getToday())) {
            throw new BadRequestException("Invalid dates");
        }
        return Rooms.getAvailableRooms(fromDate, toDate);
    }

    @Override
    public List<Reservation> getReservations(int userId) {
        return Reservations.getInstance().getReservationsForUserId(userId);
    }

    @Override
    public int makeReservation(List<String> roomNumbers, String dateFrom, String dateTo, String notes, int userId) throws BadRequestException {
        if (roomNumbers.isEmpty()) {
            throw new BadRequestException("At least one room required to make reservation");
        }
        List<Room> rooms;
        Date from = new Date(dateFrom);
        Date to = new Date(dateTo);
        try {
            rooms = roomNumbers.stream()
                    .map(number -> Rooms.findByNumber(number))
                    .collect(Collectors.toList());
        } catch (NoSuchElementException ex) {
            throw new BadRequestException("No such room in database");
        }
        if (from.after(to) || from.before(getToday())) {
            throw new BadRequestException("Invalid dates");
        }
        try {
            return Reservations.getInstance().create(rooms, from, to, notes, userId);
        } catch (RoomUnavailableException ex) {
            throw new BadRequestException("At least one room is unavailable");
        }
    }

    @Override
    public void modifyReservation(ModifiedReservation modifiedReservation, int userId) throws BadRequestException {
        Reservation reservation = new Reservation();
        reservation.setNumber(modifiedReservation.getNumber());
        reservation.setFrom(new Date(modifiedReservation.getFrom()));
        reservation.setTo(new Date(modifiedReservation.getTo()));
        reservation.setRooms(modifiedReservation.getRooms());
        reservation.setOwnersId(modifiedReservation.getOwnersId());
        reservation.setNotes(modifiedReservation.getNotes());

        Reservations reservations = Reservations.getInstance();
        try {
            if (reservations.findByNumber(reservation.getNumber()).getOwnersId() != userId) {
                throw new BadRequestException("Reservation with this number does not exist");
            }
        } catch (NoSuchElementException ex) {
            throw new BadRequestException("Reservation with this number does not exist");
        }
        if (reservation.getFrom().after(reservation.getTo()) || reservation.getFrom().before(getToday())) {
            throw new BadRequestException("Invalid dates");
        }
        try {
            reservations.modifyReservation(reservation);
        } catch (RoomUnavailableException ex) {
            throw new BadRequestException("Room(s) unavailablet");
        }
    }

    @Override
    public void cancelReservation(int reservationNumber, int userId) throws BadRequestException {
        Reservations reservations = Reservations.getInstance();
        Reservation reservation = reservations.findByNumber(reservationNumber);
        if (reservation.getOwnersId() != userId) {
            throw new BadRequestException("Reservation with this number does not exist");
        }
        reservations.remove(reservation);
    }


    private static Font catFont = new Font(Font.FontFamily.TIMES_ROMAN, 18,
            Font.BOLD);
    private static Font subFont = new Font(Font.FontFamily.TIMES_ROMAN, 16,
            Font.BOLD);
    private static Font smallBold = new Font(Font.FontFamily.TIMES_ROMAN, 12,
            Font.BOLD);

    /**
     * @param reservationNumber
     * @param userId
     * @return
     * @throws BadRequestException
     * @throws DocumentException
     */
    @Override
    public byte[] requestReservationConfirmation(int reservationNumber, int userId) throws BadRequestException, DocumentException {
        try {
            String path = System.getProperty("user.home") + File.separator + "OneDrive" + File.separator + "Pulpit" + File.separator + "iTextHelloWorld.pdf";
            System.out.println("Zapisuję do: " + path);
            String ab = path;
            Reservations reservations = Reservations.getInstance();
            Reservation reservation = reservations.findByNumber(reservationNumber);
            User user = Users.findUser(userId);

            Document document = new Document();
            PdfWriter.getInstance(document, new FileOutputStream(path));
            document.open();

            Paragraph title = new Paragraph();
            addEmptyLine(title, 1);
            title.add(new Paragraph("Potwierdzenie rezerwacji numer " + reservationNumber, catFont));
            addEmptyLine(title, 3);
            title.add(new Paragraph("Szanowny " + user.getUsername() + "!", subFont));
            addEmptyLine(title, 1);
            title.add(new Paragraph("Serdecznie dziekujemy za zrobienie rezerwacji w terminie " + reservation.getFrom() + " - " + reservation.getTo(), subFont));
            addEmptyLine(title, 1);
            title.add(new Paragraph("Szczególy rezerwacji:", subFont));
            addEmptyLine(title, 2);

            document.add(title);

            //tabela
            Paragraph details = new Paragraph();

            createTable(details, reservation);
            addEmptyLine(details, 2);
            document.add(details);

            Paragraph notes = new Paragraph();
            notes.add(new Paragraph("Dodatkowe uwagi:", subFont));
            notes.add(new Paragraph(reservation.getNotes(), smallBold));
            document.add(notes);
            document.close();

            File file = new File(path);
            byte[] b = Files.readAllBytes(Paths.get(path));
            return b;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    private static void createTable(Paragraph par, Reservation res)
            throws BadElementException {
        PdfPTable table = new PdfPTable(5);

        PdfPCell c1 = new PdfPCell(new Phrase("Numer pokoju"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Numer pietra"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Podwójne lózko"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Powierzchnia"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);

        c1 = new PdfPCell(new Phrase("Opis"));
        c1.setHorizontalAlignment(Element.ALIGN_LEFT);
        table.addCell(c1);
        table.setHeaderRows(1);

        int numberOfRooms = res.getRooms().size();
        List<Room> rooms = res.getRooms();
        for (int i = 0; i < numberOfRooms; i++) {
            Room room = rooms.get(i);

            table.addCell(room.getRoomNumber());
            table.addCell(String.valueOf(room.getFloorNumber()));
            if (room.hasDoubleBed()) {
                table.addCell("+");
            } else {
                table.addCell("-");
            }
            table.addCell(String.valueOf(room.getRoomSize()));
            table.addCell(room.getDescription());
        }


        par.add(table);

    }

    private static void addEmptyLine(Paragraph paragraph, int number) {
        for (int i = 0; i < number; i++) {
            paragraph.add(new Paragraph(" "));
        }
    }

    private Date getToday() {
        Calendar c = Calendar.getInstance();

        c.set(Calendar.HOUR_OF_DAY, 0);
        c.set(Calendar.MINUTE, 0);
        c.set(Calendar.SECOND, 0);
        c.set(Calendar.MILLISECOND, 0);

        return c.getTime();
    }
}

