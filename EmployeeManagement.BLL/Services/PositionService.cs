using System.Collections.Generic;
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

        public IEnumerable<Position> GetAllPositions()
        {
            return _unitOfWork.PositionRepository.GetAll();
        }

        public Position? GetPositionById(int id)
        {
            return _unitOfWork.PositionRepository.GetById(id);
        }

        public void AddPosition(Position position)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            _unitOfWork.PositionRepository.Add(position);
            _unitOfWork.Save();
        }

        public void UpdatePosition(Position position)
        {
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            var existing = _unitOfWork.PositionRepository.GetById(position.PositionId);
            if (existing == null)
                throw new Exception("Position not found.");

            _unitOfWork.PositionRepository.Update(position);
            _unitOfWork.Save();
        }

        public void DeletePosition(int id)
        {
            var position = _unitOfWork.PositionRepository.GetById(id);
            if (position == null)
                throw new Exception("Position not found.");

            _unitOfWork.PositionRepository.Delete(id); 
            _unitOfWork.Save();
        }
    }
}
