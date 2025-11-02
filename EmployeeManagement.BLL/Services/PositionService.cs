using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.BLL.Services
{
    public class PositionService : IPositionService

    {
        private readonly IUnitOfWork _unitOfWork;
        public PositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public void DeletePosition(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Position> GetAllPositions()
        {
            throw new NotImplementedException();
        }

        public Position? GetPositionById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
