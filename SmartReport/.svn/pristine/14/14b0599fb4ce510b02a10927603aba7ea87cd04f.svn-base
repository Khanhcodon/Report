using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.FileTransfer
{
    internal static class Transfer
    {
        internal static IFileTransfer GetTransfer(FileLocation fileLocation, FileLocationSettings fileLocationSettings)
        {
            if (fileLocation.FileLocationTypeInEnum == FileLocationType.Local)
            {
                return new FileTransferLocal(fileLocation.FileLocationAddress, fileLocationSettings.Threshold);
            }

            return new FileTransferService(fileLocation.FileLocationAddress);
        }
    }
}
