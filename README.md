### Доработка проекта https://github.com/jva44ka/MathExpressionCalculator 

-Изменен алгоритм парсинга. Теперь вычисления происходят не сразу после парсинга какой-то лексемы, а после построения дерева и в отдельной сущности. Также операторы инкапсулированы в отдельную сущность. 

-Сделана инверсия зависимостей в контрактах классов TreeBuilder и Calculator.

-Класс с основной логикой TreeBuilder покрыт юнит-тестами. Он имеет множество приватных методов, тем не менее покрыт тестами через рефлексию.
