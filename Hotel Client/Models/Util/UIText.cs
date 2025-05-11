using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Client.Models.Util
{
    public static class UIText
    {
        // === UI Labels ===
        //Login
        public const string NameLoginPage = "Hotel";
        public const string LoginTitle = "Zaloguj się";
        public const string LoginButton = "Zaloguj";
        //Home
        public const string MenuTitle = "Opcje";
        public const string HomeTitle = "Wyszukiwanie dostępnych pokoi";
        public const string SearchButton = "Wyszukaj";

        //Home MakeReservation
        public const string FromDate = "Od";
        public const string ToDate = "Do";
        public const string AddButton = "Dodaj";
        public const string DeleteButton = "Usuń";
        public const string RoomNumberFormat = "Numer pokoju: {0}";
        public const string RoomSizeFormat = "Wielkość pokoju: {0}";
        public const string DescriptionFormat = "Opis: {0}";

        //MakeReservation
        public const string ReservationTitle = "Zarezerwuj";
        public const string ReservationButton = "Zarezerwuj";

        //Reservations
        public const string ReservationsTitle = "Moje rezerwacje";
        public const string ReservationNumberFormat = "Numer rezerwacji: {0}";
        public const string CancelButton = "Anuluj";
        public const string ShowConfirmationButton = "Pokaż potwierdzenie";
        public const string FromDateFormat = "Od: {0:d}";
        public const string ToDateFormat = "Do: {0:d}";
        public const string RoomsCountFormat = "Liczba pokoi: {0}";
        public const string NotesFormat = "Notatka: {0}";
        public const string DetailsButton = "Szczegóły";

        // === Menu ===
        public const string MenuAccount = "Konto";
        public const string MenuMakeReservation = "Nowa rezerwacja";
        public const string MenuGetReservations = "Moje rezerwacje";
        public const string MenuLogout = "Wyloguj";

        // === Alerts ===
        public const string ErrorTitle = "Error";
        public const string ConfirmTitle = "Potwierdzenie";
        public const string InfoTitle = "Info";
        public const string MissingRoomMessage = "Wybierz przynajmniej jeden pokój!";
        public const string InvalidLoginMessage = "Nieprawidłowa nazwa użytkownika lub hasło.";
        public const string CloseTitle = "Zamknij";


        public const string ReservationNumberLabel = "Numer rezerwacji: ";
        public const string FromDateLabel = "Od: ";
        public const string ToDateLabel = "Do: ";
        public const string RoomsCountLabel = "Liczba pokoi: ";
        public const string NotesLabel = "Notatka: ";
        public const string ModifyDateLabel = "Zmień termin";
        public const string SaveChangesButton = "Zapisz zmiany";
        public const string AdditionalNotesLabel = "Dodatkowe notatki:";

        //Room details
        public const string RoomDetailsTitle = "Szczegóły pokoju";
        public const string RoomNumberLabel = "Numer pokoju:";
        public const string FloorNumberLabel = "Numer piętra:";
        public const string HasDoubleBedLabel = "Podwójne łóżko:";
        public const string SingleBedsLabel = "Pojedyncze łóżka:";
        public const string RoomSizeLabel = "Wielkość pokoju:";
        public const string RoomSizeUnit = " m²";
        public const string HasBathroomLabel = "Łazienka w pokoju:";
        public const string PresidentialSuiteLabel = "Apartament prezydencki:";
        public const string WindowDirectionLabel = "Kierunek okien:";
        public const string DescriptionLabel = "Opis:";
        public const string AddToReservationButton = "Dodaj do rezerwacji";
    }
}
