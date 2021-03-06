﻿//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using Microsoft.Phone.Info;

namespace SimplyMobile.Core
{
    /// <summary>
    /// Mobile application class
    /// </summary>
    public partial class MobileApp
    {
        /// <summary>
        /// Gets the memory usage of the application in bytes.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Int64"/>.
        /// </returns>
        public static long CurrentMemoryUsage { get { return DeviceStatus.ApplicationCurrentMemoryUsage; } }

        /// <summary>
        /// Gets the peak memory usage of the application in bytes.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Int64"/>.
        /// </returns>
        public static long PeakMemoryUsage { get { return DeviceStatus.ApplicationPeakMemoryUsage; } }

        /// <summary>
        /// Gets the maximum amount of memory application process can allocate in bytes.
        /// </summary>
        /// <returns>
        /// Returns <see cref="T:System.Int64"/>.
        /// </returns>
        public static long MemoryUsageLimit { get { return DeviceStatus.ApplicationMemoryUsageLimit; } }
    }
}