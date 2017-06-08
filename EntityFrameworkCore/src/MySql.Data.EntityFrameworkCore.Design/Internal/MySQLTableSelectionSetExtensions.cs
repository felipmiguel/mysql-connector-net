﻿// Copyright © 2017 Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using Microsoft.EntityFrameworkCore.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySql.Data.EntityFrameworkCore.Design.Internal
{
  public static class MySQLTableSelectionSetExtensions
  {
    public static bool Allows(this TableSelectionSet _tableSelectionSet, string schemaName, string tableName)
    {
      if (_tableSelectionSet == null
          || (_tableSelectionSet.Schemas.Count == 0
          && _tableSelectionSet.Tables.Count == 0))
      {
        return true;
      }

      var result = false;

      if (_tableSelectionSet.Schemas.Count == 0)
        result = true;

      foreach (var schemaSelection in _tableSelectionSet.Schemas)
      {
        if (schemaSelection.Text.Equals(schemaName))
        {
          schemaSelection.IsMatched = true;
          result = true;
        }
      }

      if (_tableSelectionSet.Tables.Count > 0 && result)
      {
        result = false;
        foreach (var tableSelection in _tableSelectionSet.Tables)
        {
          var components = tableSelection.Text.Split('.');
          if (components.Length == 1
              ? components[0].Equals(tableName)
              : components[0].Equals(schemaName) && components[1].Equals(tableName))
          {
            tableSelection.IsMatched = true;
            result = true;
          }
        }
      }

      return result;
    }
  }
}