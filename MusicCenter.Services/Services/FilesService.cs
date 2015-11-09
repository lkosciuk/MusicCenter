using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace MusicCenter.Services.Services
{
    public class FilesService : BaseService<Files>, IFilesService
    {
        public FilesService(IUnitOfWork u)
            : base(u)
        {
            
        }

        public Files AddFile(string FileName, string Path)
        {
            Files newFile = new Files()
            {
                name = FileName,
                path = Path,
                ObjectState = ObjectState.Added
            };

            _repo.Insert(newFile);
            _unitOfWork.SaveChanges();

            return newFile;
        }

    }
}
