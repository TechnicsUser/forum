/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2019 Ingo Herbote
 * http://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Types.Interfaces
{
    using YAF.Types.Objects;

    /// <summary>
    /// The ThankYou interface.
    /// </summary>
    public interface IThankYou
    {
        /// <summary>
        /// This method returns a string containing the HTML code for
        ///   showing the the post footer. the HTML content is the name of users
        ///   who thanked the post and the date they thanked.
        /// </summary>
        /// <param name="messageId">
        /// The message Id.
        /// </param>
        /// <returns>
        /// The get thanks.
        /// </returns>
        [NotNull]
        string GetThanks([NotNull] int messageId);

        /// <summary>
        /// Creates an instance of the thank you object from the current information.
        /// </summary>
        /// <param name="username">
        /// The Current Username
        /// </param>
        /// <param name="textTag">
        /// Button Text
        /// </param>
        /// <param name="titleTag">
        /// Button  Title
        /// </param>
        /// <param name="messageId">
        /// The message Id.
        /// </param>
        /// <returns>
        /// Returns ThankYou Info
        /// </returns>
        [NotNull]
        ThankYouInfo CreateThankYou(
            [NotNull] string username,
            [NotNull] string textTag,
            [NotNull] string titleTag,
            int messageId);

        /// <summary>
        /// This method returns a string which shows how many times users have
        ///   thanked the message with the provided messageID. Returns an empty string.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="messageID">
        /// The Message ID.
        /// </param>
        /// <returns>
        /// The thanks number.
        /// </returns>
        string ThanksNumber([NotNull] string username, int messageID);
    }
}