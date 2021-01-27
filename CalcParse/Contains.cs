namespace CalcParse
{	
	/// <summary>
	/// Содержит инструменты для поиска соответствия символов операторам
	/// или цифрам.
	/// </summary>
	public static class Contains
	{
		private static char[] numbers { get; } = {'0', '1', '2', '3',
			'4', '5', '6', '7', '8', '9'};              // Все цифры
		private static char[] operators { get; } = {'*', '/', '+',
			'-', '÷', '×', '(', ')', '.', ','};         // Все операторы
		private static char[] mathOperators { get; } = { '*', '/', '+',
			'-', '÷', '×' };                // Все математические операторы
		private static char[] brackets { get; } = { '(', ')' };   // Все скобки
		private static char[] separators { get; } = { ',', '.' }; // Все разделители

		/// <summary>
		/// Поиск символа в коллекции цифр.
		/// </summary>
		/// <param name="findSymbol">Символ, который нужно найти.</param>
		/// <returns>Был ли найден символ в коллекции.</returns>
		public static bool ToNumbers(char findSymbol)
		{
			foreach (char symbol in numbers)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Поиск символа в коллекции всех операторов.
		/// (Сюда так же относятся скобки и разделители дроби.)
		/// </summary>
		/// <param name="findSymbol">Символ, который нужно найти.</param>
		/// <returns>Был ли найден символ в коллекции.</returns>
		public static bool ToAllOperators(char findSymbol)
		{
			foreach (char symbol in operators)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Поиск символа в коллекции математических операторов.
		/// </summary>
		/// <param name="findSymbol">Символ, который нужно найти.</param>
		/// <returns>Был ли найден символ в коллекции.</returns>
		public static bool ToMathOperators(char findSymbol)
		{
			foreach (char symbol in mathOperators)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Поиск символа в коллекции скобок.
		/// </summary>
		/// <param name="findSymbol">Символ, который нужно найти.</param>
		/// <returns>Был ли найден символ в коллекции.</returns>
		public static bool ToBrackets(char findSymbol)
		{
			foreach (char symbol in brackets)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Поиск символа в коллекции разделителей дроби.
		/// </summary>
		/// <param name="findSymbol">Символ, который нужно найти.</param>
		/// <returns>Был ли найден символ в коллекции.</returns>
		public static bool ToSeparators(char findSymbol)
		{
			foreach (char symbol in separators)
			{
				if (findSymbol == symbol)
					return true;
			}
			return false;
		}
	}
}
