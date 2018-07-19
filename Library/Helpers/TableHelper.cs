using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Helpers
{
    public static class TableHelper
    {
        public static MvcHtmlString CreateTable<T>(this HtmlHelper helper, string title,  IEnumerable<T> collection, Func<T, string> additionalField = null)
        {
            TagBuilder table = new TagBuilder("table");     //Создание таблицы
            table.AddCssClass("userTable");                 //Добавления класа таблицы

            TagBuilder caption = new TagBuilder("caption"); //Создание заголовка

            TagBuilder h = new TagBuilder("h3");            //Создание типа заголовка
            h.InnerHtml = title;                            //Добавление текста заголовка

            caption.InnerHtml = h.ToString();               //Добавление в заголовок текст заголовка
            table.InnerHtml += caption.ToString();          //Добавление в талицу заголовка

            TagBuilder trHead = new TagBuilder("tr");       //Создание строки заголовков столбцов таблицы

            //Проходимся по всем свойствам модели и добавляем их в заголовоки столбцов
            foreach (var propInfo in typeof(T).GetProperties())
            {
                //Получение Display(Name="sometext") из модели
                var type = typeof(T);
                var memInfo = type.GetMember(propInfo.Name); // your member
                var attributes = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
                var displayname = ((System.ComponentModel.DataAnnotations.DisplayAttribute)attributes[0]).Name;

                //Создание столбцов заголовков таблицы
                TagBuilder th = new TagBuilder("th")
                {
                    InnerHtml = displayname
                };

                trHead.InnerHtml += th.ToString();          //Добавление в строку заголовков таблицы
            }

            //Если дополнительное поле передано то создаем дополнитеьлный столбез для действий
            if (additionalField != null)
            {
                trHead.InnerHtml += (new TagBuilder("th") { InnerHtml = "Дейсвия" }).ToString();
            }

            table.InnerHtml += trHead.ToString();           //Добавление в таблицу строки с заголовками столбцов

            TagBuilder tBody = new TagBuilder("tbody");     //Создание тела таблицы

            //Если коллекция не пустая то проходимся по коллекции
            if (collection?.Any() == true)
            {
                foreach (var item in collection)
                {
                    TagBuilder trTBody = new TagBuilder("tr");      //Создаем строку в теле таблицы

                    //Проходим по свойствам элемента коллекции
                    foreach (var propInfo in typeof(T).GetProperties())
                    {
                        //Создаем столбец в текущей строке элемента коллекции
                        TagBuilder tdBody = new TagBuilder("td")
                        {
                            InnerHtml = propInfo.GetValue(item).ToString()
                        };

                        trTBody.InnerHtml += tdBody.ToString();     //Добавляем в строку созданный столбец
                    }

                    //Если дополнительное поле не пустое, то добавляем данные в дополнительный столбец
                    if (additionalField != null)
                    {
                        trTBody.InnerHtml += (new TagBuilder("td") { InnerHtml = additionalField(item) }).ToString();
                    }

                    
                    tBody.InnerHtml += trTBody.ToString();          //Добавляем в тело таблицы созданную строку со столбцами
                }
            }

            table.InnerHtml += tBody.ToString(); //Добавляем в таблицу тело со строками

            return new MvcHtmlString(table.ToString());
        }
    }
}