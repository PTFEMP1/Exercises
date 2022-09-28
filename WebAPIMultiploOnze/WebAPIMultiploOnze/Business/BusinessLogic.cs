namespace WebAPIMultiploOnze.Business
{
    public static class BusinessLogic
    {
        private static readonly string[] TwoDigitsMultipleOfEleven = new[]
        {
            "11", "22", "33", "44", "55", "66", "77", "88", "99"
        };
        /// <summary>
        /// Logica para Verificar se um numero em formato String é Multiplo de 11
        /// </summary>
        /// <param name="number">Numero em formato de String</param>
        /// <returns></returns>
        public static bool isMultipleOfEleven(string number)
        {
            bool result = false;

            //Se o numero for menor que 10 nunca vai ser multiplo de 11
            if (number.Length < 2)
            {
                return result;
            }

            int totalSecDigits = 0;
            int totalFirstDigits = 0;

            // Caso Number tenha 2 algarismos verifica se é multiplo de 11 usando uma lista definida acima
            if (number.Length == 2)
            {
                return TwoDigitsMultipleOfEleven.Contains(number);
            }

            // Caso Number tenha 3 algarismos é efetuado um calculo especifico para derterminar  se é ou não multiplo de 11
            for (int i = 0; i < number.Length; i++)
            {
                //Pessoalmente prefiro este codigo que Apesar não ser o mais "readable" deverá ter melhor perfomance
                //totalFirstDigits = c[i]-48 + totalFirstDigits;
                //totalSecDigits = c[i+1] - 48 + totalSecDigits;
                
                if (i % 2 == 0)
                {
                    totalFirstDigits = (int)Char.GetNumericValue(number[i]) + totalFirstDigits;
                }
                else
                {
                    totalSecDigits = (int)Char.GetNumericValue(number[i]) + totalSecDigits;
                }
            }

            int total = totalFirstDigits - totalSecDigits;
            if (total < 0)
            {
                total = total * -1;
            }

            result = total == 0 || TwoDigitsMultipleOfEleven.Contains(total.ToString());
            return result;
        }

    }
}
