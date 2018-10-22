using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Moxy.EntityFramework.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyDeletedFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetInterface(nameof(ISoftDeletable)) == null)
                    continue;

                // 1. Add the Deleted property
                entityType.GetOrAddProperty("Deleted", typeof(bool));

                // 2. Create the query filter

                var parameter = Expression.Parameter(entityType.ClrType);

                // EF.Property<bool>(post, "Deleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var deletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("Deleted"));

                // EF.Property<bool>(post, "Deleted") == false
                var compareExpression =
                    Expression.MakeBinary(ExpressionType.Equal, deletedProperty, Expression.Constant(false));

                // post => EF.Property<bool>(post, "Deleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly, params string[] @namespace)
        {
            //筛选出继承自IEntityTypeConfiguration的类型
            var types = assembly.GetTypes().Where(t => !t.IsAbstract && t.GetInterfaces().Any(IsIEntityTypeConfigurationType));
            var typeModelBuilder = modelBuilder.GetType();
            var methodNonGenericApplyConfiguration = typeModelBuilder
                .GetMethods().First(m => m.IsGenericMethod && m.Name == nameof(ModelBuilder.ApplyConfiguration) &&
                                         m.GetParameters().Any(s => s.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in types)
            {
                if (@namespace != null && !@namespace.Contains(type.Namespace))
                {
                    continue;
                }

                var entityTypeConfig = Activator.CreateInstance(type);
                //获取实体的类型
                var typeEntity = type.GetInterfaces().First(IsIEntityTypeConfigurationType).GenericTypeArguments[0];
                //通过MakeGenericMethod转换为泛型方法
                var methodApplyConfiguration = methodNonGenericApplyConfiguration.MakeGenericMethod(typeEntity);
                methodApplyConfiguration.Invoke(modelBuilder, new[] { entityTypeConfig });
            }
        }

        private static bool IsIEntityTypeConfigurationType(Type typeIntf)
        {
            return typeIntf.IsInterface && typeIntf.IsGenericType && typeIntf.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);
        }
    }
}
