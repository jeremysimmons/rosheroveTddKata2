﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddKata
{
    public class StringCalculator
    {
        List<int> _invalidNumbers;
        List<ProcessValueAction> _numberStrageies;
        private ILogger _logger;
        private IWebService _service;
        delegate void ProcessValueAction(int newNumber, ref int aggregate, ref bool stopProcessing);

        public StringCalculator()
            : this(NullLogger.Default)
        {
        }

        public StringCalculator(ILogger logger)
            : this(logger, NullWebService.Default)
        {
        }

        public StringCalculator(ILogger logger, IWebService service)
        {
            _logger = logger;
            _service = service;
            _invalidNumbers = new List<int>();
            _numberStrageies = new List<ProcessValueAction>();
            _numberStrageies.Add(ThrowOnNegativeValues);
            _numberStrageies.Add(SkipNumbersLargerThan1000);
            _numberStrageies.Add(AggregateNewNumber);

        }

        private static void AggregateNewNumber(int newNumber, ref int aggregate, ref bool stopProcessing)
        {
            aggregate += newNumber;
        }

        private static void SkipNumbersLargerThan1000(int newNumber, ref int aggregate, ref bool stopProcessing)
        {
            if (newNumber > 1000)
                stopProcessing = true;
        }

        private void ThrowOnNegativeValues(int newNumber, ref int aggregate, ref bool stopProcessing)
        {
            if (newNumber < 0)
            {
                _invalidNumbers.Add(newNumber);
            }
        }

        public int Add(string inputs)
        {
            CalculationExpressionParser parser = new CalculationExpressionParser();
            string[] delimiter = parser.Delimiters(inputs);
            string[] allSeparators = delimiter.Concat(new[] { "\n" }).ToArray();
            string[] strings = inputs.Split(allSeparators, StringSplitOptions.RemoveEmptyEntries);

            int temp = 0;
            int result = 0;
            foreach (string number in strings)
            {
                if (Int32.TryParse(number, out temp))
                {
                    bool stopProcessing = false;
                    foreach (ProcessValueAction numberStragey in _numberStrageies)
                    {
                        numberStragey(temp, ref result, ref stopProcessing);
                        if (stopProcessing)
                            break;
                    }
                }
            }

            if (_invalidNumbers.Count > 0)
                throw new ArgumentException("Invalid Numbers: " + String.Join(", ", _invalidNumbers.Select(x => x.ToString())));

            try
            {
                _logger.Write("Sum Result: " + result);
            }
            catch (Exception ex)
            {
                _service.Notify(ex.Message);
            }
            return result;
        }
    }
}
