﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SlimFormaturas.Domain.Dto.Seller;
using SlimFormaturas.Domain.Dto.Seller.Response;
using SlimFormaturas.Domain.Entities;
using SlimFormaturas.Domain.Interfaces.Service;
using SlimFormaturas.Domain.Notifications;

namespace SlimFormaturas.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ApiController {
        readonly ISellerService _sellerService;
        readonly IMapper _mapper;
        public SellerController (
            ISellerService sellerService,
            NotificationHandler notifications,
            IMapper mapper) : base(notifications) {
            _sellerService = sellerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Inserir um novo formando na base teste
        /// </summary>
        /// <returns>O id do novo formando inserido</returns>
        //[CustomAuthorize.ClaimsAuthorize("Graduate", "Incluir")]
        [HttpPost]
        public async Task<ActionResult<SellerResponse>> Post ([FromBody] SellerForCreationDto sellerForCreationDto) {
            var seller = _mapper.Map<Seller>(sellerForCreationDto);
            return Response(await _sellerService.Insert(seller));
        }

        //[CustomAuthorize.ClaimsAuthorize("Graduate", "Editar")]
        [HttpPut]
        public async Task<ActionResult<SellerResponse>> Put ([FromBody] SellerDto sellerDto) {
            return Response(await _sellerService.Update(sellerDto));
        }

        // GET api/Graduate
        /// <summary>
        /// Get all graduates
        /// </summary>
        /// <remarks>This API will get the values.</remarks>
        /// 
       // [CustomAuthorize.ClaimsAuthorizeAttribute("Graduate", "Consultar")]
        [HttpGet]
        public async Task<ActionResult<SellerDto>> Get () {
            var sellers = _mapper.Map<IList<SellerDto>>(await _sellerService.Get());
            return Response(sellers);

        }

        //[CustomAuthorize.ClaimsAuthorize("Graduate", "Consultar")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> Get (string id) {
            var seller = _mapper.Map<SellerDto>(await _sellerService.GetAllById(id));
            return Response(seller);
        }
    }
}
