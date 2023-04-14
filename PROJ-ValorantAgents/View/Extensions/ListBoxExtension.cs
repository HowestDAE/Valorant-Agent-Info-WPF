using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace PROJ_ValorantAgents.View.Extensions
{
    public class ListBoxExtension
    {
        public static readonly DependencyProperty InitialIndexOnItemsSourceChangedProperty;
        private static readonly IDictionary<ListBox, BindingSpy<ListBox, IEnumerable>> ComboBoxToBindingSpiesMapping = new Dictionary<ListBox, BindingSpy<ListBox, IEnumerable>>();


        static ListBoxExtension()
        {
            InitialIndexOnItemsSourceChangedProperty = DependencyProperty.RegisterAttached("InitialIndexOnItemsSourceChanged",
                                                                                           typeof(int?),
                                                                                           typeof(ListBoxExtension),
                                                                                           new FrameworkPropertyMetadata(null, OnInitialIndexOnItemsSourceChanged));
        }

        public static void SetInitialIndexOnItemsSourceChanged(ListBox targetComboBox, int? value)
        {
            if (targetComboBox == null) throw new ArgumentNullException("targetComboBox");

            targetComboBox.SetValue(InitialIndexOnItemsSourceChangedProperty, value);
        }

        public static int? GetInitialIndexOnItemsSourceChanged(ListBox targetComboBox)
        {
            if (targetComboBox == null) throw new ArgumentNullException("targetComboBox");

            return (int?)targetComboBox.GetValue(InitialIndexOnItemsSourceChangedProperty);
        }

        private static void OnInitialIndexOnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var targetComboBox = d as ListBox;
            if (targetComboBox == null)
                return;

            if ((int?)e.NewValue != null)
            {
                SetInitialIndexIfPossible(targetComboBox);
                EstablishBindingSpy(targetComboBox);
                return;
            }

            ReleaseBindingSpy(targetComboBox);
        }

        private static void EstablishBindingSpy(ListBox targetComboBox)
        {
            if (ComboBoxToBindingSpiesMapping.ContainsKey(targetComboBox))
                return;

            var bindingSpy = new BindingSpy<ListBox, IEnumerable>(targetComboBox, ItemsControl.ItemsSourceProperty);
            bindingSpy.TargetValueChanged += OnItemsSourceChanged;
            ComboBoxToBindingSpiesMapping.Add(targetComboBox, bindingSpy);
        }

        private static void ReleaseBindingSpy(ListBox targetComboBox)
        {
            if (ComboBoxToBindingSpiesMapping.ContainsKey(targetComboBox) == false)
                return;

            var bindingSpy = ComboBoxToBindingSpiesMapping[targetComboBox];
            bindingSpy.ReleaseBinding();
            ComboBoxToBindingSpiesMapping.Remove(targetComboBox);
        }

        private static void OnItemsSourceChanged(BindingSpy<ListBox, IEnumerable> bindingSpy)
        {
            SetInitialIndexIfPossible(bindingSpy.TargetObject);
        }

        private static void SetInitialIndexIfPossible(ListBox targetComboBox)
        {
            var initialIndexOnItemsSourceChanged = GetInitialIndexOnItemsSourceChanged(targetComboBox);
            if (targetComboBox.ItemsSource != null && initialIndexOnItemsSourceChanged.HasValue)
            {
                targetComboBox.SelectedIndex = initialIndexOnItemsSourceChanged.Value;
            }
        }


        public class BindingSpy<TSource, TValue> : DependencyObject where TSource : DependencyObject
        {
            private readonly TSource _targetObject;
            private readonly DependencyProperty _targetProperty;

            public static readonly DependencyProperty TargetValueProperty = DependencyProperty.Register("TargetValue",
                                                                                                        typeof(TValue),
                                                                                                        typeof(BindingSpy<TSource, TValue>),
                                                                                                        new FrameworkPropertyMetadata(null, OnTargetValueChanged));

            public BindingSpy(TSource targetObject, DependencyProperty targetProperty)
            {
                if (targetObject == null) throw new ArgumentNullException("targetObject");
                if (targetProperty == null) throw new ArgumentNullException("targetProperty");
                _targetObject = targetObject;
                _targetProperty = targetProperty;

                var binding = new Binding
                {
                    Source = targetObject,
                    Path = new PropertyPath(targetProperty),
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(this, TargetValueProperty, binding);
            }

            public TValue TargetValue
            {
                get { return (TValue)GetValue(TargetValueProperty); }
                set { SetValue(TargetValueProperty, value); }
            }

            public TSource TargetObject
            {
                get { return _targetObject; }
            }

            public DependencyProperty TargetProperty
            {
                get { return _targetProperty; }
            }

            public void ReleaseBinding()
            {
                BindingOperations.ClearBinding(this, TargetValueProperty);
            }

            private static void OnTargetValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var bindingSpy = d as BindingSpy<TSource, TValue>;
                if (bindingSpy == null)
                    return;

                if (bindingSpy.TargetValueChanged != null)
                    bindingSpy.TargetValueChanged(bindingSpy);
            }

            public event Action<BindingSpy<TSource, TValue>> TargetValueChanged;
        }
    }
}
