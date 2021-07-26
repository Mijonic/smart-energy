using Microsoft.AspNetCore.Http;
using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IMultimediaService
    {
        public Task AttachFileToWorkRequestAsync(IFormFile formFile, int workRequestId);
        public Task AttachFileToIncidentAsync(IFormFile formFile, int incidentId);
        public Task AttachFileToSafetyDocumentAsync(IFormFile formFile, int safetyDocId);
        public Task ScanAttachmentAsync(IFormFile formFile);
        public FileStream GetWorkRequestAttachmentStream(int workRequestId, string fileName);
        public FileStream GetIncidentAttachmentStream(int incidentId, string fileName);
        public FileStream GetSafetyDocumentAttachmentStream(int safetyDocId, string fileName);
        public List<MultimediaAttachmentDto> GetWorkRequestAttachments(int workRequestId);
        public List<MultimediaAttachmentDto> GetIncidentAttachments(int incidentId);
        public List<MultimediaAttachmentDto> GetSafetyDocumentAttachments(int safetyDocId);
        public void DeleteWorkRequestAttachment(int workRequestId, string filename);
        public void DeleteSafetyDocumentAttachment(int safetyDocId, string filename);
        public void DeleteIncidentAttachment(int incidentId, string filename);
        public Task AttachUserAvatar(IFormFile formFile, int userId);
        public FileStream GetUserAvatarStream(int userId, string imageURL);
        public void DeleteWorkRequestFileOnDisk(int workRequestID, string filePath);
        public void DeleteIncidentFileOnDisk(int workRequestID, string filePath);
        public void DeleteSafetyDocumentFileOnDisk(int safetyDocID, string filePath);
    }
}
