using BlackRiver.EntityModels;
using System;

namespace BlackRiver.Desktop.Models
{
    [Serializable]
    internal class ReservationConflictException : Exception
    {
        public Reserva ExistingReservation { get; }
        public Reserva IncomingReservation { get; }

        public ReservationConflictException(Reserva existingReservation, Reserva incomingReservation)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string message, Reserva existingReservation, Reserva incomingReservation) : base(message)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string message, Exception innerException, Reserva existingReservation, Reserva incomingReservation) : base(message, innerException)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }
    }
}