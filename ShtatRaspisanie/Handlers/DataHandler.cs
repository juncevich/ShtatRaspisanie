using System.Collections.Generic;
using System.Linq;
using ShtatRaspisanie.Entities;

namespace ShtatRaspisanie.Handlers
{
    public class DataHandler : IDataHandler
    {
        private List<Unit> _units;

        public List<Unit> Units
        {
            get { return _units; }
        }

        // Список штатных единиц.
        private List<StaffUnit> _staffUnits;

        //Соотнесение категориям подкатегорий,
        //подкатегориям и категориям штатных единиц.
        public List<Unit> HandleUnitTable(List<Unit> listOfString, List<StaffUnit> staffUnits)
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
            var units = new List<Unit>();

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
                    parentUnit.Name = item.Name;
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
                    unit.Name = item.Name;
                    unit.Parent = item.Parent;
                    //Получаем список штатных единиц дочернего элемента.
                    unit.StaffUnits = GetStaffUnits(unit.Name);
                    //Добавляем к родительскому элементу.
                    parentUnitMain.Child.Add(unit);
                    //Сохраняем, как последний обработанный.
                    lastItem = unit;
                    //Находим дочерние для дочерних элементов.
                }
                else if (!ReferenceEquals(item.Parent, "") && ReferenceEquals(item.Parent, lastItem.Name))
                {
                    //Получаем список штатных единиц для элемента.
                    item.StaffUnits = GetStaffUnits(item.Name);
                    //Добавляем в последний элемент.
                    lastItem.Child.Add(item);
                }
            }
            _units = units;
            return units;
        }

        //Поиск штатных единиц. Возвращает список ШЕ,
        //привязанных к имени элемента.
        public List<StaffUnit> GetStaffUnits(string unitName)
        {
            return _staffUnits.Where(item => item.PodrName.Equals(unitName)).ToList();
        }
    }
}