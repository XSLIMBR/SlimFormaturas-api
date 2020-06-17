﻿using AutoMapper;
using SlimFormaturas.Domain.Dto.ShippingCompany;
using SlimFormaturas.Domain.Entities;
using SlimFormaturas.Domain.Interfaces.Repository;
using SlimFormaturas.Domain.Interfaces.Service;
using SlimFormaturas.Domain.Notifications;
using SlimFormaturas.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SlimFormaturas.Service.Services {
    public class ShippingCompanyService : BaseService<ShippingCompany>, IShippingCompanyService {
        private readonly IShippingCompanyRepository _shippingCompanyRepository;
        protected readonly NotificationHandler _notifications;
        protected readonly ITypeGenericRepository _typeGenericRepository;
        readonly IMapper _mapper;

        public ShippingCompanyService (
            IShippingCompanyRepository shippingCompanyRepository,
            NotificationHandler notifications,
             IMapper mapper,
            ITypeGenericRepository typeGenericRepository) : base(shippingCompanyRepository) {
            _shippingCompanyRepository = shippingCompanyRepository;
            _notifications = notifications;
            _typeGenericRepository = typeGenericRepository;
            _mapper = mapper;
        }

        public async Task<ShippingCompany> Insert (ShippingCompany obj) {

            obj.Validate(obj, new ShippingCompanyValidator());
            _notifications.AddNotifications(obj.ValidationResult);

            foreach (var item in obj.Address) {
                item.Validate(item, new AddressValidator());
                _notifications.AddNotifications(item.ValidationResult);
            }

            foreach (var item in obj.Phone) {
                item.Validate(item, new PhoneValidator());
                _notifications.AddNotifications(item.ValidationResult);
            }

            if (!_notifications.HasNotifications) {
                await Post(obj);
            }

            return obj;
        }

        public async Task<ShippingCompany> Update (ShippingCompanyDto shippingCompanyDto) {

            ShippingCompany shippingCompany = await _shippingCompanyRepository.GetAllById(shippingCompanyDto.ShippingCompanyId);

            _mapper.Map(shippingCompanyDto, shippingCompany);

            shippingCompany.Validate(shippingCompany, new ShippingCompanyValidator());
            _notifications.AddNotifications(shippingCompany.ValidationResult);

            foreach (var item in shippingCompany.Address) {
                item.Validate(item, new AddressValidator());
                _notifications.AddNotifications(item.ValidationResult);
            }

            foreach (var item in shippingCompany.Phone) {
                item.Validate(item, new PhoneValidator());
                _notifications.AddNotifications(item.ValidationResult);
            }

            if (!_notifications.HasNotifications) {
                await Put(shippingCompany);
            }

            return shippingCompany;
        }

        public async Task<ShippingCompany> GetAllById (string id) {
            return await _shippingCompanyRepository.GetAllById(id);
        }
    }
}
