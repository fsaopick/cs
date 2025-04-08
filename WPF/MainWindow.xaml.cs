using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private StringBuilder expressionBuilder = new StringBuilder();
        private List<string> expressionTokens = new List<string>();
        private bool displayResult = false;
        private bool displayError = false;
        private int openBracketsCount = 0;
        private CultureInfo culture = CultureInfo.InvariantCulture;

        public MainWindow()
        {
            InitializeComponent();
            ClearAll();
        }

        private void ClearAll()
        {
            expressionBuilder.Clear();
            expressionTokens.Clear();
            openBracketsCount = 0;
            displayResult = false;
            displayError = false;
            ExpressionTextBlock.Text = "";
            ResultTextBlock.Text = "0";
        }

        private void ClearEntry()
        {
            if (displayResult || displayError)
            {
                ClearAll();
            }
            else
            {
                ResultTextBlock.Text = "0";
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayError)
            {
                ClearAll();
                return;
            }

            if (displayResult)
            {
                ClearAll();
                return;
            }

            if (ResultTextBlock.Text.Length > 1)
            {
                ResultTextBlock.Text = ResultTextBlock.Text.Substring(0, ResultTextBlock.Text.Length - 1);
            }
            else
            {
                ResultTextBlock.Text = "0";
            }
        }

        private void AddToExpression(string value)
        {
            if (displayResult || displayError)
            {
                ClearAll();
            }

            expressionBuilder.Append(value);
            ExpressionTextBlock.Text = expressionBuilder.ToString();
        }

        private void UpdateResult(string value)
        {
            ResultTextBlock.Text = value;
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayResult)
            {
                ClearAll();
            }

            var button = (Button)sender;
            string digit = button.Content.ToString();

            if (ResultTextBlock.Text == "0" || displayResult)
            {
                ResultTextBlock.Text = digit;
            }
            else
            {
                ResultTextBlock.Text += digit;
            }
            displayResult = false;
            displayError = false;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayResult)
            {
                ClearAll();
            }

            if (!ResultTextBlock.Text.Contains("."))
            {
                if (ResultTextBlock.Text == string.Empty || ResultTextBlock.Text == "-")
                {
                    ResultTextBlock.Text += "0.";
                }
                else
                {
                    ResultTextBlock.Text += ".";
                }
            }
            displayResult = false;
            displayError = false;
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayResult || displayError)
            {
                ClearAll();
            }

            if (ResultTextBlock.Text.StartsWith("-"))
            {
                ResultTextBlock.Text = ResultTextBlock.Text.Substring(1);
            }
            else
            {
                if (ResultTextBlock.Text != "0")
                {
                    ResultTextBlock.Text = "-" + ResultTextBlock.Text;
                }
            }
        }

        private void FunctionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string function = button.Content.ToString();
            double value = double.Parse(ResultTextBlock.Text, culture);
            double result = 0;

            try
            {
                switch (function)
                {
                    case "sin":
                        result = Math.Sin(value * Math.PI / 180);
                        AddToExpression($"sin({value})");
                        break;
                    case "cos":
                        result = Math.Cos(value * Math.PI / 180);
                        AddToExpression($"cos({value})");
                        break;
                    case "tg":
                        result = Math.Tan(value * Math.PI / 180);
                        AddToExpression($"tg({value})");
                        break;
                    case "x²":
                        result = Math.Pow(value, 2);
                        AddToExpression($"({value})²");
                        break;
                    case "√x":
                        if (value < 0) throw new ArgumentException("Корень из отрицательного числа");
                        result = Math.Sqrt(value);
                        AddToExpression($"√({value})");
                        break;
                    case "1/x":
                        if (value == 0) throw new DivideByZeroException();
                        result = 1 / value;
                        AddToExpression($"1/({value})");
                        break;
                    case "|x|":
                        result = Math.Abs(value);
                        AddToExpression($"abs({value})");
                        break;
                    case "n!":
                        result = Factorial((int)value);
                        AddToExpression($"fact({value})");
                        break;
                    case "10^x":
                        result = Math.Pow(10, value);
                        AddToExpression($"10^({value})");
                        break;
                    case "log":
                        if (value <= 0) throw new ArgumentException("Логарифм от неположительного числа");
                        result = Math.Log10(value);
                        AddToExpression($"log({value})");
                        break;
                    case "ln":
                        if (value <= 0) throw new ArgumentException("Натуральный логарифм от неположительного числа");
                        result = Math.Log(value);
                        AddToExpression($"ln({value})");
                        break;
                }

                UpdateResult(result.ToString(culture));
                displayResult = true;
            }
            catch (Exception ex)
            {
                displayError = true;
                UpdateResult("Error");
                ExpressionTextBlock.Text = ex.Message;
            }
        }

        private int Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Факториал отрицательного числа");
            if (n > 20) throw new OverflowException("Факториал слишком большой");
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string op = button.Content.ToString();

            if (op == "×") op = "*";
            if (op == "÷") op = "/";
            if (op == "x^y") op = "^";

            expressionTokens.Add(ResultTextBlock.Text);
            expressionTokens.Add(op);
            AddToExpression(ResultTextBlock.Text + op);

            UpdateResult("0");
            displayResult = false;
        }

        private void BracketButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string bracket = button.Content.ToString();

            if (bracket == ")")
            {
                if (openBracketsCount <= 0)
                {
                    displayError = true;
                    UpdateResult("Error");
                    ExpressionTextBlock.Text = "Несовпадающие круглые скобки";
                    return;
                }

                if (ResultTextBlock.Text != "0")
                {
                    expressionTokens.Add(ResultTextBlock.Text);
                    expressionBuilder.Append(ResultTextBlock.Text);
                }

                openBracketsCount--;
            }
            else
            {
                if (expressionTokens.Count > 0 &&
                    double.TryParse(expressionTokens[expressionTokens.Count - 1], NumberStyles.Any, culture, out _))
                {
                    expressionTokens.Add("*");
                    expressionBuilder.Append("*");
                }
                openBracketsCount++;
            }

            expressionTokens.Add(bracket);
            expressionBuilder.Append(bracket);
            ExpressionTextBlock.Text = expressionBuilder.ToString();
            UpdateResult("0");
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (openBracketsCount != 0)
                {
                    throw new Exception("Несбалансированные круглые скобки");
                }

                if (ResultTextBlock.Text != "0")
                {
                    expressionTokens.Add(ResultTextBlock.Text);
                    expressionBuilder.Append(ResultTextBlock.Text);
                    ExpressionTextBlock.Text = expressionBuilder.ToString();
                }

                double result = EvaluateExpression(expressionTokens);
                UpdateResult(result.ToString(culture));
                displayResult = true;
            }
            catch (Exception ex)
            {
                displayError = true;
                UpdateResult("Error");
                ExpressionTextBlock.Text = ex.Message;
            }
        }

        private double EvaluateExpression(List<string> tokens)
        {
            var output = new List<string>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, culture, out _))
                {
                    output.Add(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Pop();
                }
                else
                {
                    while (operators.Count > 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop());
            }

            var stack = new Stack<double>();
            foreach (var token in output)
            {
                if (double.TryParse(token, NumberStyles.Any, culture, out double number))
                {
                    stack.Push(number);
                }
                else
                {
                    double b = stack.Pop();
                    double a = stack.Pop();
                    stack.Push(ApplyOperator(token, a, b));
                }
            }

            return stack.Pop();
        }

        private int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 0;
            }
        }

        private double ApplyOperator(string op, double a, double b)
        {
            switch (op)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/":
                    if (b == 0) throw new DivideByZeroException();
                    return a / b;
                case "^": return Math.Pow(a, b);
                default: throw new ArgumentException($"Неизвестный оператор: {op}");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry();
        }

        private void ConstantButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string constant = button.Content.ToString();

            switch (constant)
            {
                case "π":
                    UpdateResult(Math.PI.ToString(culture));
                    AddToExpression("π");
                    break;
                case "e":
                    UpdateResult(Math.E.ToString(culture));
                    AddToExpression("e");
                    break;
            }
            displayResult = true;
        }
    }
}