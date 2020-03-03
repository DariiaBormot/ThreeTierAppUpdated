﻿using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using PresentationLayer.Interfaces;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class CarController : ICarController
    {
        private readonly CarService carService;

        public CarController()
        {
            carService = new CarService();
        }
        public void Create(CarViewModel carView)
        {
            var carModels = carService.GetCars().ToList();

            if (IsUniqueName(carView, carModels)&&LessThanTwoSpaces(carView.Name))
            {
                CreateCar(carView);
            }
        }

        private void CreateCar(CarViewModel model)
        {
            var carModel = new CarModel()
            {
                Id = model.Id,
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
                TotalPrice = model.TotalPrice,
                Details = model.Details?.Select(x => new DetailModel
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    Name = x.Name,
                    Price = x.Price,
                    DetailTypeId = x.DetailTypeId,
                    ManufacturerId = x.ManufacturerId
                })
            };
            carService.Create(carModel);
        }

        private bool LessThanTwoSpaces(string str)
        {
            char c = ' ';
            var result =  str.Count(x => x == c) <= 2;
            return result;
        }

        private bool IsUniqueName(CarViewModel model, List<CarModel> models)
        {
            var result =  models.All(x => x.Name != model.Name);
            return result; 
        }

        public void Delete(int id)
        {
            carService.Delete(id);
        }

        public CarViewModel GetById(int id)
        {
            var car = carService.GetById(id);
            var carViewModel = new CarViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                ManufacturerId = car.ManufacturerId,
                TotalPrice = car.TotalPrice,
                Details = car.Details.Select(d => new DetailViewModel()
                {
                    Id = d.Id,
                    CarId = d.CarId,
                    Name = d.Name,
                    Price = d.Price,
                    DetailTypeId = d.DetailTypeId,
                    ManufacturerId = d.ManufacturerId
                })
            };
            return carViewModel;
        }

        public IEnumerable<CarViewModel> GetCars()
        {
            var cars = carService.GetCars();
            var carsViews = cars.Select(car => new CarViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                ManufacturerId = car.ManufacturerId,
                TotalPrice = car.TotalPrice,
                Details = car.Details.Select(detail => new DetailViewModel()
                {
                    Id = detail.Id,
                    CarId = detail.CarId,
                    Name = detail.Name,
                    DetailTypeId = detail.DetailTypeId,
                    ManufacturerId = detail.ManufacturerId,
                    Price = detail.Price
                })
            });
            return carsViews;
        }

        public void Update(CarViewModel carView)
        {
            var carModel = new CarModel()
            {
                Id = carView.Id,
                Name = carView.Name,
                ManufacturerId = carView.ManufacturerId,
                TotalPrice = carView.TotalPrice,
                Details = carView.Details?.Select(x => new DetailModel
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    Name = x.Name,
                    Price = x.Price,
                    DetailTypeId = x.DetailTypeId,
                    ManufacturerId = x.ManufacturerId
                })
            };
            carService.Update(carModel);
        }

        public IEnumerable<CarViewModel> GetMostExpensiveCars()
        {
            var cars = carService.GetMostExpensiveCars();
            var carsViews = cars.Select(car => new CarViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                ManufacturerId = car.ManufacturerId,
                TotalPrice = car.TotalPrice,
                Details = car.Details.Select(detail => new DetailViewModel()
                {
                    Id = detail.Id,
                    CarId = detail.CarId,
                    Name = detail.Name,
                    Price = detail.Price,
                    DetailTypeId = detail.DetailTypeId,
                    ManufacturerId = detail.ManufacturerId
                })

            });

            return carsViews;
        }
    }

}
