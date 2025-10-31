using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;

namespace EmployeeManagement.BLL.Services
{
    public interface IPosittionService
    {
        IEnumerable<Position> GetAllPositions();
        Position? GetPositionById(int id);
        void AddPosition(Position position);
        void UpdatePosition(Position position);
        void DeletePosition(int id);
    }
}
