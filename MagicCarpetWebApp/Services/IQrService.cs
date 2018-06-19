using System;

namespace MagicCarpetWebApp.Services
{
    public interface IQrService
    {
        string GetTicketCode(Guid reservationId);
    }
}