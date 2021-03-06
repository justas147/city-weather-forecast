﻿using AutoMapper;
using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using city_weather_forecast_API.Repositories;
using city_weather_forecast_API.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City, string> _repository;
        private readonly IMapper _mapper;

        public CityService(IRepository<City, string> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<City> Create(City newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            if (_repository.DoesEntityExist(newItem.PlaceCode))
                return newItem;

            await _repository.Create(newItem);
            return newItem;
        }

        public Task<bool> Delete(string id)
        {
            return _repository.Delete(id);
        }

        public Task<bool> DeleteAll()
        {
            return _repository.DeleteAll();
        }

        public async Task<ICollection<CityDetailsDTO>> GetAll()
        {
            var cities = await _repository.GetAll();
            var citiesDto = _mapper.Map<CityDetailsDTO[]>(cities);

            return citiesDto;
        }

        public async Task<CityDetailsDTO> GetById(string id)
        {
            var city = await _repository.GetById(id);
            var cityDto = _mapper.Map<CityDetailsDTO>(city);

            return cityDto;
        }

        public async Task Update(string id, City updateData)
        {
            if (updateData == null) throw new ArgumentNullException(nameof(updateData));

            if (id != updateData.PlaceCode)
            {
                return;
            }

            if (!_repository.DoesEntityExist(updateData.PlaceCode))
                return;

            await _repository.Update(id, updateData);
            return; 
        }
    }
}
