﻿namespace MainTz.Application.Models
{
    public class Description
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string MaxSpeed { get; set; }
        public string FrontWheelDrive { get; set; } // привод
        public string EnginePower { get; set; } //Мощность двигателя
        public string Guarantee { get; set; } //Гарантия
        public string KPP { get; set; } //Автоматическая/ручная
        public string OilType { get; set; } //Тип топлива / бензин/газ
        public int Count { get; set; } // Кол-во машин
        public string ShortDescription { get; set; } // Описание


        public Car Car { get; set; }
    }
}
