using System.Runtime.CompilerServices;

namespace Calculator.View;

public partial class CalculatorPage : ContentPage
{
    delegate double Operator(double x, double y);
	delegate double Operation(double x);
	readonly Dictionary<string, Operator> operators = new Dictionary<string, Operator>()
	{
		{"+", (x, y) => x + y },
		{"-", (x, y) => x - y },
		{"*", (x, y) => x * y },
		{"/", (x, y) => x / y },
		{"^", (x, y) => Math.Pow(x, y) }
	};
	readonly Dictionary<string, Operation> operations = new Dictionary<string, Operation>()
	{
		{"%", x => x * 0.01},
        {"1/x", x => 1 / x},
        {"x^2", x => Math.Pow(x, 2)},
        {"x^(1/2)", x => Math.Pow(x, 0.5)},
        {"+/-", x => -x},
    };
	readonly List<Button> disablingButtonsForError;

	readonly LinkedList<string> memory = new LinkedList<string>();

	bool isEuqalMode = false;
	bool isOperatorMode = false;
	bool isError = false;

	string currentNum = "0";
	string prevNum = "";
	string currentOperator = "";
	string tempNum = "";
	string errorMessage = "";
	public CalculatorPage()
	{
		InitializeComponent();

        disablingButtonsForError = new List<Button>()
        {
            Button_Percent, Button_1x, Button_x2, Button_sqrt, Button_div,
            Button_mul, Button_minus, Button_plus, Button_sign, Button_dot
        };

        UpdateUI();

    }

	void UpdateUI()
	{
		if (isError)
		{
			LabelCurrentNum.Text = errorMessage;
			ChangeButtonsForError(false);
        }
		else
		{
            LabelCurrentNum.Text = currentNum;
        }

        if (isEuqalMode) 
		{
			LabelPrevNum.Text = tempNum + " " + currentOperator + " " + prevNum + " =";
        }
		else
		{
            LabelPrevNum.Text = prevNum + " " + currentOperator;
        }

		Button_MC.IsEnabled = memory.Count > 0;
        Button_M.IsEnabled = memory.Count > 0;
        Button_MR.IsEnabled = memory.Count > 0;
    }

	void NumberClick(object sender, EventArgs e)
	{
		if (isError)
		{
            isError = false;
            currentNum = "0";
            currentOperator = "";
            prevNum = "";
            ChangeButtonsForError(true);
        }

		if (isEuqalMode)
		{
			isEuqalMode = false;
			currentNum = "0";
			currentOperator = "";
			prevNum = "";
		}

		if (isOperatorMode)
		{
			isOperatorMode = false;
			currentNum = "0";
		}

		if (currentNum.Equals("0")) 
		{
			currentNum = "";
		}

		currentNum += (sender as Button)?.Text ?? "";

		this.UpdateUI();
	}
	void DotClick(object sender, EventArgs e)
	{
        if (isEuqalMode)
        {
            isEuqalMode = false;
            currentNum = "0";
            currentOperator = "";
            prevNum = "";
        }

        if (isOperatorMode)
        {
            isOperatorMode = false;
            currentNum = "0";
        }

        currentNum += ",";
		UpdateUI();
	}
	void OperatorClick(object sender, EventArgs e)
	{
		if (isEuqalMode) 
		{
			isEuqalMode= false;
			currentOperator = "";
		}

		if (currentOperator != "" && !isOperatorMode)
		{
            double x = double.Parse(prevNum);
            double y = double.Parse(currentNum);

            double res = operators[currentOperator](x, y);

			currentNum = res.ToString();
        }

		string operat = (sender as Button)?.Text ?? "";
		
		if (operat == "x^y")
		{
			operat = "^";
		}

        prevNum = currentNum;
		currentOperator = operat;
        isOperatorMode = true;
		this.UpdateUI();
    }
	void OperationClick(object sender, EventArgs e)
	{
        if (isEuqalMode)
        {
            isEuqalMode = false;
            prevNum = "";
            currentOperator = "";
        }
        string operation = (sender as Button)?.Text ?? "";

		if (!operations.ContainsKey(operation))
		{
			return;
		}

		double oldNum = Double.Parse(currentNum);

		if (operation == "1/x" && oldNum == 0)
		{
			isError = true;
			errorMessage = "Can't Divide By Zero!";
			currentNum = "0";
            UpdateUI();
			return;
        }

		if (operation == "x^(1/2)" && oldNum < 0)
		{
			isError = true;
			errorMessage = "Invalid Input!";
			currentNum = "0";
			UpdateUI();
			return;
		}

		double newNum = operations[operation](oldNum);

		currentNum = newNum.ToString();
		UpdateUI();
	}

	void EqualClick(object sender, EventArgs e)
	{
		if (isError)
		{
			isError = false;
			isOperatorMode = false;
			isEuqalMode = false;
            prevNum = "";
            currentOperator = "";
            currentNum = "0";
            ChangeButtonsForError(true);
            this.UpdateUI();
			return;
        }

		if (!operators.ContainsKey(currentOperator))
		{
			return;
		}

        if (isEuqalMode)
        {
            string temp = prevNum;
            prevNum = currentNum;
            currentNum = temp;
        }

        double x = double.Parse(prevNum);
		double y = double.Parse(currentNum);

		if (currentOperator == "/" && y == 0)
		{
			isError = true;
			errorMessage = "Can't Divide By Zero";
			currentNum = "0";
			this.UpdateUI();
		}

		if(currentOperator == "^" && (x < 0 || y < 0 || (x == 0 && y == 0)))
		{
            isError = true;
            errorMessage = "Invalid Input!";
            currentNum = "0";
            this.UpdateUI();
        }

		double res = operators[currentOperator](x, y);

		if (res == double.PositiveInfinity || res == double.NegativeInfinity)
		{
            isError = true;
            errorMessage = "Infinty";
            currentNum = "0";
            this.UpdateUI();
        }

        if (res == double.NaN)
        {
            isError = true;
            errorMessage = "NaN";
            currentNum = "0";
            this.UpdateUI();
        }

        isEuqalMode = true;
        tempNum = prevNum;
        prevNum = currentNum;
		currentNum = res.ToString();
		isOperatorMode = false;
		this.UpdateUI();
	}
	void ClearEntryClick(object sender, EventArgs e)
	{
		if (isError)
		{
			isError = false;
			prevNum = "";
			currentOperator = "";
			isOperatorMode = false;
			isEuqalMode =  false;
            ChangeButtonsForError(true);
        }
		else if (isEuqalMode)
		{
			currentNum = "0";
			currentOperator = "";
			prevNum = "";
			isEuqalMode = false;
		}
		currentNum = "0";
		isOperatorMode = false;
		UpdateUI();
	}
	void ClearClick(object sender, EventArgs e)
	{
		currentNum = "0";
		currentOperator = "";
		prevNum = "";
        isEuqalMode = false;
		isOperatorMode = false;
		isError = false;
		ChangeButtonsForError(true);
        this.UpdateUI();
	}
	void BackSpaceClick(object sender, EventArgs e)
	{
		if (isError)
		{
			isError = false;
            isOperatorMode = false;
            isEuqalMode = false;
            prevNum = "";
            currentOperator = "";
			currentNum = "0";
            ChangeButtonsForError(true);
        }
		else if (isOperatorMode)
		{
            this.UpdateUI();
			return;
        }
		else if (isEuqalMode)
		{
			prevNum = "";
			currentOperator = "";
			isEuqalMode = false;
		}
		else if (currentNum.Length == 1)
		{
			currentNum = "0";
		}
		else
		{
			currentNum = currentNum.Remove(currentNum.Length - 1);
		}
		this.UpdateUI();
	}

	void ChangeButtonsForError(bool release)
	{
		foreach(var button in disablingButtonsForError)
		{
			button.IsEnabled = release;
		}
	}

	void MemoryClearClick(object sender, EventArgs e)
	{
		memory.Clear();

		if (isEuqalMode)
		{
			isEuqalMode = false;
			currentOperator = "";
			prevNum = "";
		}

		this.UpdateUI();
	}
	void MemoryRecallClick(object sender, EventArgs e)
	{
		currentNum = memory?.First?.Value ?? "0";

        if (isEuqalMode)
        {
            isEuqalMode = false;
            currentOperator = "";
            prevNum = "";
        }

        this.UpdateUI();
	}
	void MemoryAddClick(object sender, EventArgs e)
	{
		if (memory.Count == 0)
		{
			memory.AddFirst(currentNum);
		}
		else
		{
			double temp = Double.Parse(memory?.First?.Value ?? "0");
			temp += Double.Parse(currentNum);
			memory!.First!.Value = temp.ToString();
		}

        if (isEuqalMode)
        {
            isEuqalMode = false;
            currentOperator = "";
            prevNum = "";
        }

        this.UpdateUI();
	}
    void MemoryMinusClick(object sender, EventArgs e)
    {
        if (memory.Count == 0)
        {
            memory.AddFirst("-" + currentNum);
        }
        else
        {
            double temp = Double.Parse(memory?.First?.Value ?? "0");
            temp -= Double.Parse(currentNum);
            memory!.First!.Value = temp.ToString();
        }

        if (isEuqalMode)
        {
            isEuqalMode = false;
            currentOperator = "";
            prevNum = "";
        }

        this.UpdateUI();
    }
	void MemoryStoreClick(object sender, EventArgs e)
	{
		memory.AddFirst(currentNum);

        if (isEuqalMode)
        {
            isEuqalMode = false;
            currentOperator = "";
            prevNum = "";
        }

        this.UpdateUI();
    }
	void MemoryShowClick(object sender, EventArgs e)
	{
        DisplayActionSheet(
			"Memory values",
			"Cancel",
			null,
			memory.ToArray()
		);
        this.UpdateUI();
    }

}