using System;
using System.Collections.Generic;
using System.Linq;
using OSRS.Core.Models.Enums;
using OSRS.Domain.Entities;
using OSRS.Resources.Localization;
using OSRS.Application.Seed.Output;

namespace OSRS.Application.Seed
{
    public static class TreeModelExtensions {
        // public static List<TreeModel<T>> BuildTree<T>(this IEnumerable<ProductosObject> source) where T: IConvertible {
        //     List<TreeModel<T>> list = new();
        //     var roots = source.GroupBy(i => new { i.IdTipoProducto});
        //     if (roots.Any()) {
        //         var collection = roots.ToDictionary(g => g.Key, g => g.ToList());
        //         foreach (var item in collection) {
        //             var tree = new TreeModel<T>(){
        //                 Value = (T)Convert.ChangeType(item.Key.IdTipoProducto, typeof(T)),
        //                 Label = OSRS.Infrastructure.Helper.Utils.GetEnumValues<TipoProductos>().Where(x => x == item.Key.IdTipoProducto)
        //                     .Select(x => I18n.ResourceManager.GetString($"{x.ToString()}") ?? x.ToString()).First(), //. item.Key.IdTipoProducto.ToString(), //item.Key.Name,
        //                 Children = item.Value.Select(s => new TreeNodeModel<T>(){
        //                     Value = (T)Convert.ChangeType(s.IdProducto, typeof(T)),
        //                     Label = s.Nombre
        //                 }).ToList()
        //             };
        //             list.Add(tree);
        //         }
        //     }
        //     return list;
        // }
        
        // public static List<TreeModel<T>> BuildTree<T>(this IEnumerable<Category> source) where T : IConvertible {
        //     return source?.Select(p => new TreeModel<T>() {
        //         Value = (T)Convert.ChangeType(p.Code, typeof(T)),
        //         Text = p.Name,
        //         Children = p.Children.Select(c => new TreeNodeModel<T>() {
        //             Value = (T)Convert.ChangeType(c.Code, typeof(T)),
        //             Text = c.Name
        //         }).ToList()
        //     }).ToList() ?? new List<TreeModel<T>>();
        // }
        // public static List<TreeNodeModel<T>> BuildTreeNode<T>(this IEnumerable<Category> source) where T : IConvertible
        // {
        //     return source?.Select(p => new TreeNodeModel<T>() {
        //         Value = (T)Convert.ChangeType(p.Code, typeof(T)),
        //         Text = p.Name
        //     }).ToList() ?? new List<TreeNodeModel<T>>();
        // }
        // public static List<TreeNodeModel<T>> BuildTreeNode<T>(this IEnumerable<BaseSupplierObject> source) where T : IConvertible
        // {
        //     return source?.Select(p => new TreeNodeModel<T>(){
        //         Value = (T)Convert.ChangeType(p.Code, typeof(T)),
        //         Text = p.Name
        //     }).ToList() ?? new List<TreeNodeModel<T>>();
        // }
    }
}
