using System.Diagnostics;

namespace CSharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0039:ローカル関数を使用します", Justification = "<保留中>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:値の不必要な代入", Justification = "<保留中>")]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ラムダ式を使用して匿名関数を作成する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            /// ラムダ式は、2つの形式のいずれかになります。
            /// 式形式のラムダ
            /// 　(input-parameters) => expression
            /// 　
            /// ステートメント形式のラムダ
            /// 　(input-parameters) => { <sequence-of-statements> }

            /// ラムダ式は、デリゲート型に変換できます。
            /// ラムダ式が値を返さない場合は`Action`デリゲート型のいずれかに変換できます。
            /// 値を返す場合は`Func`デリゲート型のいずれかに変換できます。
            /// 次の例では、`x`というパラメータを指定し、`x`の二乗の値を返すラムダ式です。
            Func<int, int> square = x => x * x;
            Debug.WriteLine(square(5));

            /// 式形式のラムダは、次の例のように、式ツリー型にも変換できます。
            System.Linq.Expressions.Expression<Func<int, int>> exp = x => x * x;
            Debug.WriteLine(exp);

            /// ラムダ式は、デリゲート型または式ツリーのインスタンスを必要とする全てのコードで使用できます。
            /// また、次のように、C#でLINQを作成する場合にも使用できます。
            int[] numbers = { 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select(x => x * x);
            Debug.WriteLine(string.Join(", ", squaredNumbers));
        }

        /// <summary>
        /// ステートメント形式のラムダ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            /// ステートメント形式のラムダは式形式のラムダに似ていますが、
            /// ステートメントが中括弧で囲まれている点が異なります。
            /// (input-parameters) => { <sequence-of-statements> }

            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}";
                Debug.WriteLine(greeting);
            };
            greet("world");
        }

        /// <summary>
        /// ラムダ式の入力パラメーター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            /// ラムダ式の入力パラメータが無いことを指定するには、
            /// 次のように空の括弧を使用します。
            Action line = () => Console.WriteLine("Hello");

            /// 入力パラメータが1つしかない場合は、括弧は省略可能です。
            Func<double, double> cube = x => x * x * x;

            /// 入力パラメータが2つ以上の場合は、コンマで区切ります。
            Func<int, int, bool> func = (x, y) => x == y;

            /// コンパイラで型推論が出来ない場合は、型を明示的に指定できます。
            Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;

            /// C# 9.0以降では、`破棄`を使用して
            /// 式で使用しないラムダ式の2つ以上の入力パラメータを指定できます。
            Func<int, int, int> constant = (_, _) => 100;

            /// C# 12以降では、パラメータに`規定値`を使用できます。
            var IncrementBy = (int source, int increment = 1) => source + increment;

            /// パラメータとして配列を使用し、ラムダ式を宣言することも可能です。
            var sum = (params int[] values) =>
            {
                int sum = 0;
                foreach (var value in values)
                    sum += value;

                return sum;
            };

            var empty = sum();
            Debug.WriteLine("empty:" + empty);

            var sequence = new[] { 1, 2, 3, 4, 5 };
            var total = sum(sequence);
            Debug.WriteLine("total:" + total);
        }

        /// <summary>
        /// 非同期ラムダ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            /// 非同期ラムダを使用して、同じイベントハンドラーを追加できます。
            /// 次の例に示すように、ラムダパラメータリストの前に`async`修飾子を追加します。
            button1.Click += async (sender, e) =>
            {
                await ExampleMethodAsync();
                Debug.WriteLine("ボタン1をクリックして1秒後に表示");
            };

            Debug.WriteLine("ボタン1にイベントハンドラーを追加しました。");
        }

        /// <summary>
        /// 非同期確認用
        /// </summary>
        /// <returns></returns>
        private static async Task ExampleMethodAsync()
        {
            await Task.Delay(1000);
        }

        /// <summary>
        /// ラムダ式とタプル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            /// C#には`タプル`のサポートが組み込まれています。
            /// タプルは、ラムダ式への引数として指定できるほか、ラムダ式で返すこともできます。
            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            var numbers = (2, 3, 4);
            var doubledNumbers = doubleThem(numbers);

            Debug.WriteLine($"The set {numbers} doubled : {doubledNumbers}");
        }

        /// <summary>
        /// 標準クエリ演算子を使用したラムダ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            /// ①
            /// いくつかある実装の中で特に、`LINQ to Objects`は、汎用デリゲートの
            /// `Func<TResult>`ファミリに属する型の入力パラメータを持ちます。
            /// Funcデリゲートは、ソースデータのセット内の各要素に適用されるユーザ定義の式を
            /// カプセル化する場合に便利です。例えば、Func<T, TResult>デリゲート型です。

            /// public delegate TResult Func<in T, out TResult>(T arg)

            /// このデリゲートを`Func<int, bool>`としてインスタンス化できます。
            /// 戻り値は必ず最後の型パラメータで指定されます。
            /// 例えば、`Func<int, string, bool>`では、intとstringの2つの入力パラメータと
            /// 戻り値の型`bool`を持つデリゲートを定義しています。
            Func<int, string, bool> equalsLength = (i, s) => s.Length == i;
            bool result = equalsLength(5, "result");
            Debug.WriteLine("①:" + result);

            /// ②
            /// 例えば、`Queryable`型で定義された標準クエリ演算子において、
            /// 引数型が`Expression<TDelegate>`の場合もラムダ式を使用できます。
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Debug.WriteLine($"②:{string.Join(", ", numbers)}から、奇数のものは{oddNumbers}個");

            /// ③
            /// 次の例では、配列内で9より前にある要素を取得します。
            var firstNumbersLessThanSix = numbers.TakeWhile(n => n < 6);
            Debug.WriteLine("③:" + string.Join(", ", firstNumbersLessThanSix));

            /// ④
            /// 次の例では、要素の値を比較し、後ろの値の方が大きい場合が発生するまでの要素を取得します。
            var firstSamllNumbers = numbers.TakeWhile((n, index) => n >= index);
            Debug.WriteLine("④:" + string.Join(", ", firstSamllNumbers));

            /// ⑤
            /// ラムダ式を`クエリ式`で直接使用することはありませんが、
            /// 次のように、クエリ式内のメソッドで呼び出して使用できます。
            var numberSets = new List<int[]>
            {
                new[] { 1, 2, 3, 4, 5 },
                new[] { 0, 0, 0 },
                new[] { 9, 8 },
                new[] { 1, 0, 1, 0, 1, 0, 1, 0 }
            };

            var setWithManyPositives =
                from numberSet in numberSets
                where numberSet.Count(n => n > 0) > 3
                select numberSet;  // 0より大きい整数を3つ含む配列を取得

            foreach (var numberSet in setWithManyPositives)
            {
                Debug.WriteLine("⑤:" + string.Join(", ", numberSet));
            }
        }
    }
}