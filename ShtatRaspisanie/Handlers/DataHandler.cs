using ShtatRaspisanie.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        // Список штатных единиц.
        private List<StaffUnit> _staffUnits; 
        //Соотнесение категориям подкатегорий, 
        //подкатегориям и категориям штатных единиц.
        public ArrayList HandleUnitTable(List<Unit> listOfString, List<StaffUnit> staffUnits)
        {
            //Загружаем список штатных единиц.
            _staffUnits = staffUnits;
            //Создаем р>дительское подразделение.
            ParentUnit parentUnitMain = new ParentUnit();
            //Инициализируем пустыми значениями.
            parentUnitMain.Name = "";
            parentUnitMain.Parent = "";
            //Создаем элемент, который указывает
            //на последний обработанный элемент.
            Unit lastItem = new Unit();
            // TO DO Изменить коллекцию на List<IUnit>  
            var units = new ArrayList();
            
            //Перебираем значения из файла с подразделениями.
            foreach (var item in listOfString)
            {
                //Выбираем родительское подразделение
                if (item.Parent.Equals(""))
                {
                    //Создаем сам объект.
                    var parentUnit = new ParentUnit();
                    //Инициализируем поле новым пустым списком
                    //Дабы не кидало исключение.
                    parentUnit.Child = new List<Unit>();
                    //Присваиваем Имя
                    parentUnit.Name = (string)item.Name;
                    //Присваиваем пустое значение родителя.
                    parentUnit.Parent = "";
                    //Получаем список штатных единиц, привязанных к элементу
                    parentUnit.StaffUnits = GetStaffUnits(parentUnit.Name);
                    //Добавляем в список родительских элементов
                    units.Add(parentUnit);
                    //Сохраняем элемент, для дальнейшего сравнения с именем.
                    parentUnitMain = parentUnit;
                    
                }
                    //Находим дочерний элемент.
                else if (item.Parent.Equals(parentUnitMain.Name))
                {
                    //Создание и инициализация.
                    Unit unit = new Unit();
                    unit.Name = (string)item.Name;
                    unit.Parent = (string)item.Parent;
                    //Получаем список штатных единиц дочернего элемента.
                    unit.StaffUnits = GetStaffUnits(unit.Name);
                    //Добавляем к родительскому элементу.
                    parentUnitMain.Child.Add(unit);
                    //Сохраняем, как последний обработанный.
                    lastItem = unit;
                    
                } else if (!ReferenceEquals(item.Parent, "") && lastItem != null && ReferenceEquals(item.Parent, lastItem.Name))
                {
                    item.StaffUnits = GetStaffUnits(item.Name);
                    lastItem.Child.Add(item);
                    Unit unit = new Unit();

                }
            }

            return units;
        }

        public List<StaffUnit> GetStaffUnits(string unitName)
        {
            List<StaffUnit> staffUnits = new List<StaffUnit>();
            foreach (var item in _staffUnits)
            {

                if (item.PodrName.Equals(unitName))
                {
                    staffUnits.Add(item);
                }

            }

            return staffUnits;
        }
    }
}