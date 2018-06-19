using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MagicCarpetWebApp.Services
{
    public class QrService : IQrService
    {

        public string GetTicketCode(Guid reservationId)
        {
            //This is the reservationId, should actually include some payment details.
            string data = $"reservationId={reservationId}";
            return $"https://api.qrserver.com/v1/create-qr-code/?size=250x250&data={HttpUtility.UrlEncode(data)}";
        }

    }
}
