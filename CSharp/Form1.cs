using System.Diagnostics;

namespace CSharp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0039:���[�J���֐����g�p���܂�", Justification = "<�ۗ���>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:�l�̕s�K�v�ȑ��", Justification = "<�ۗ���>")]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �����_�����g�p���ē����֐����쐬����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            /// �����_���́A2�̌`���̂����ꂩ�ɂȂ�܂��B
            /// ���`���̃����_
            /// �@(input-parameters) => expression
            /// �@
            /// �X�e�[�g�����g�`���̃����_
            /// �@(input-parameters) => { <sequence-of-statements> }

            /// �����_���́A�f���Q�[�g�^�ɕϊ��ł��܂��B
            /// �����_�����l��Ԃ��Ȃ��ꍇ��`Action`�f���Q�[�g�^�̂����ꂩ�ɕϊ��ł��܂��B
            /// �l��Ԃ��ꍇ��`Func`�f���Q�[�g�^�̂����ꂩ�ɕϊ��ł��܂��B
            /// ���̗�ł́A`x`�Ƃ����p�����[�^���w�肵�A`x`�̓��̒l��Ԃ������_���ł��B
            Func<int, int> square = x => x * x;
            Debug.WriteLine(square(5));

            /// ���`���̃����_�́A���̗�̂悤�ɁA���c���[�^�ɂ��ϊ��ł��܂��B
            System.Linq.Expressions.Expression<Func<int, int>> exp = x => x * x;
            Debug.WriteLine(exp);

            /// �����_���́A�f���Q�[�g�^�܂��͎��c���[�̃C���X�^���X��K�v�Ƃ���S�ẴR�[�h�Ŏg�p�ł��܂��B
            /// �܂��A���̂悤�ɁAC#��LINQ���쐬����ꍇ�ɂ��g�p�ł��܂��B
            int[] numbers = { 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select(x => x * x);
            Debug.WriteLine(string.Join(", ", squaredNumbers));
        }

        /// <summary>
        /// �X�e�[�g�����g�`���̃����_
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            /// �X�e�[�g�����g�`���̃����_�͎��`���̃����_�Ɏ��Ă��܂����A
            /// �X�e�[�g�����g�������ʂň͂܂�Ă���_���قȂ�܂��B
            /// (input-parameters) => { <sequence-of-statements> }

            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}";
                Debug.WriteLine(greeting);
            };
            greet("world");
        }

        /// <summary>
        /// �����_���̓��̓p�����[�^�[
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            /// �����_���̓��̓p�����[�^���������Ƃ��w�肷��ɂ́A
            /// ���̂悤�ɋ�̊��ʂ��g�p���܂��B
            Action line = () => Console.WriteLine("Hello");

            /// ���̓p�����[�^��1�����Ȃ��ꍇ�́A���ʂ͏ȗ��\�ł��B
            Func<double, double> cube = x => x * x * x;

            /// ���̓p�����[�^��2�ȏ�̏ꍇ�́A�R���}�ŋ�؂�܂��B
            Func<int, int, bool> func = (x, y) => x == y;

            /// �R���p�C���Ō^���_���o���Ȃ��ꍇ�́A�^�𖾎��I�Ɏw��ł��܂��B
            Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;

            /// C# 9.0�ȍ~�ł́A`�j��`���g�p����
            /// ���Ŏg�p���Ȃ������_����2�ȏ�̓��̓p�����[�^���w��ł��܂��B
            Func<int, int, int> constant = (_, _) => 100;

            /// C# 12�ȍ~�ł́A�p�����[�^��`�K��l`���g�p�ł��܂��B
            var IncrementBy = (int source, int increment = 1) => source + increment;

            /// �p�����[�^�Ƃ��Ĕz����g�p���A�����_����錾���邱�Ƃ��\�ł��B
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
        /// �񓯊������_
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            /// �񓯊������_���g�p���āA�����C�x���g�n���h���[��ǉ��ł��܂��B
            /// ���̗�Ɏ����悤�ɁA�����_�p�����[�^���X�g�̑O��`async`�C���q��ǉ����܂��B
            button1.Click += async (sender, e) =>
            {
                await ExampleMethodAsync();
                Debug.WriteLine("�{�^��1���N���b�N����1�b��ɕ\��");
            };

            Debug.WriteLine("�{�^��1�ɃC�x���g�n���h���[��ǉ����܂����B");
        }

        /// <summary>
        /// �񓯊��m�F�p
        /// </summary>
        /// <returns></returns>
        private static async Task ExampleMethodAsync()
        {
            await Task.Delay(1000);
        }

        /// <summary>
        /// �����_���ƃ^�v��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            /// C#�ɂ�`�^�v��`�̃T�|�[�g���g�ݍ��܂�Ă��܂��B
            /// �^�v���́A�����_���ւ̈����Ƃ��Ďw��ł���ق��A�����_���ŕԂ����Ƃ��ł��܂��B
            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            var numbers = (2, 3, 4);
            var doubledNumbers = doubleThem(numbers);

            Debug.WriteLine($"The set {numbers} doubled : {doubledNumbers}");
        }

        /// <summary>
        /// �W���N�G�����Z�q���g�p���������_
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            /// �@
            /// ��������������̒��œ��ɁA`LINQ to Objects`�́A�ėp�f���Q�[�g��
            /// `Func<TResult>`�t�@�~���ɑ�����^�̓��̓p�����[�^�������܂��B
            /// Func�f���Q�[�g�́A�\�[�X�f�[�^�̃Z�b�g���̊e�v�f�ɓK�p����郆�[�U��`�̎���
            /// �J�v�Z��������ꍇ�ɕ֗��ł��B�Ⴆ�΁AFunc<T, TResult>�f���Q�[�g�^�ł��B

            /// public delegate TResult Func<in T, out TResult>(T arg)

            /// ���̃f���Q�[�g��`Func<int, bool>`�Ƃ��ăC���X�^���X���ł��܂��B
            /// �߂�l�͕K���Ō�̌^�p�����[�^�Ŏw�肳��܂��B
            /// �Ⴆ�΁A`Func<int, string, bool>`�ł́Aint��string��2�̓��̓p�����[�^��
            /// �߂�l�̌^`bool`�����f���Q�[�g���`���Ă��܂��B
            Func<int, string, bool> equalsLength = (i, s) => s.Length == i;
            bool result = equalsLength(5, "result");
            Debug.WriteLine("�@:" + result);

            /// �A
            /// �Ⴆ�΁A`Queryable`�^�Œ�`���ꂽ�W���N�G�����Z�q�ɂ����āA
            /// �����^��`Expression<TDelegate>`�̏ꍇ�������_�����g�p�ł��܂��B
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Debug.WriteLine($"�A:{string.Join(", ", numbers)}����A��̂��̂�{oddNumbers}��");

            /// �B
            /// ���̗�ł́A�z�����9���O�ɂ���v�f���擾���܂��B
            var firstNumbersLessThanSix = numbers.TakeWhile(n => n < 6);
            Debug.WriteLine("�B:" + string.Join(", ", firstNumbersLessThanSix));

            /// �C
            /// ���̗�ł́A�v�f�̒l���r���A���̒l�̕����傫���ꍇ����������܂ł̗v�f���擾���܂��B
            var firstSamllNumbers = numbers.TakeWhile((n, index) => n >= index);
            Debug.WriteLine("�C:" + string.Join(", ", firstSamllNumbers));

            /// �D
            /// �����_����`�N�G����`�Œ��ڎg�p���邱�Ƃ͂���܂��񂪁A
            /// ���̂悤�ɁA�N�G�������̃��\�b�h�ŌĂяo���Ďg�p�ł��܂��B
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
                select numberSet;  // 0���傫��������3�܂ޔz����擾

            foreach (var numberSet in setWithManyPositives)
            {
                Debug.WriteLine("�D:" + string.Join(", ", numberSet));
            }
        }
    }
}