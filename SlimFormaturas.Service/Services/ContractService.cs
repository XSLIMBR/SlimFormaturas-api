﻿using AutoMapper;
using SlimFormaturas.Domain.Entities;
using SlimFormaturas.Domain.Interfaces.Repository;
using SlimFormaturas.Domain.Interfaces.Service;
using SlimFormaturas.Domain.Notifications;
using SlimFormaturas.Domain.Validators;
using System.Threading.Tasks;

namespace SlimFormaturas.Service.Services
{
    public class ContractService : BaseService<Contract>, IContractService
    {
        private readonly IContractRepository _contractRepository;
        protected readonly NotificationHandler _notifications;
        protected readonly ITypeGenericRepository _typeGenericRepository;
        readonly IMapper _mapper;
        public ContractService(
            IContractRepository contractRepository,
            NotificationHandler notifications,
             IMapper mapper,
            ITypeGenericRepository typeGenericRepository) : base(contractRepository)
        {
            _contractRepository = contractRepository;
            _notifications = notifications;
            _typeGenericRepository = typeGenericRepository;
            _mapper = mapper;
        }

        public async Task<Contract> Insert(Contract obj)
        {
            obj.Validate(obj, new ContractValidator());
            _notifications.AddNotifications(obj.ValidationResult);
            return obj;
        }
    }
}
