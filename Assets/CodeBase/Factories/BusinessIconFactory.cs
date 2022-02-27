using UnityEngine;
using Zenject;

namespace Factories
{
    public class BusinessIconFactory : IFactory<BusinessIcon, Transform, BusinessIcon>
    {
        public BusinessIcon Create(BusinessIcon icon, Transform grid)
        {
            return Object.Instantiate(icon, grid);
        }
    }
}
