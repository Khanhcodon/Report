//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bkav.eGovCloud.SingleSignOnService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SsoToken", Namespace="http://schemas.datacontract.org/2004/07/Bkav.eGovCloud.SingleSignOnService")]
    [System.SerializableAttribute()]
    public partial class SsoToken : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ChangePasswordStatus", Namespace="http://schemas.datacontract.org/2004/07/Bkav.eGovCloud.SingleSignOnService")]
    [System.SerializableAttribute()]
    public partial class ChangePasswordStatus : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginStatus", Namespace="http://schemas.datacontract.org/2004/07/Bkav.eGovCloud.SingleSignOnService")]
    [System.SerializableAttribute()]
    public partial class LoginStatus : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool MustChangePasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordExpireDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool WarningChangePasswordField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool MustChangePassword {
            get {
                return this.MustChangePasswordField;
            }
            set {
                if ((this.MustChangePasswordField.Equals(value) != true)) {
                    this.MustChangePasswordField = value;
                    this.RaisePropertyChanged("MustChangePassword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordExpireDate {
            get {
                return this.PasswordExpireDateField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordExpireDateField, value) != true)) {
                    this.PasswordExpireDateField = value;
                    this.RaisePropertyChanged("PasswordExpireDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool WarningChangePassword {
            get {
                return this.WarningChangePasswordField;
            }
            set {
                if ((this.WarningChangePasswordField.Equals(value) != true)) {
                    this.WarningChangePasswordField = value;
                    this.RaisePropertyChanged("WarningChangePassword");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UpdateUserStatus", Namespace="http://schemas.datacontract.org/2004/07/Bkav.eGovCloud.SingleSignOnService")]
    [System.SerializableAttribute()]
    public partial class UpdateUserStatus : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SsoUser", Namespace="http://schemas.datacontract.org/2004/07/Bkav.eGovCloud.SingleSignOnService")]
    [System.SerializableAttribute()]
    public partial class SsoUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CookieDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CookieData {
            get {
                return this.CookieDataField;
            }
            set {
                if ((object.ReferenceEquals(this.CookieDataField, value) != true)) {
                    this.CookieDataField = value;
                    this.RaisePropertyChanged("CookieData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SingleSignOnService.ISingleSignOnService")]
    public interface ISingleSignOnService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnService/RequestToken", ReplyAction="http://tempuri.org/ISingleSignOnService/RequestTokenResponse")]
        Bkav.eGovCloud.SingleSignOnService.SsoToken RequestToken();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnService/LoginToken", ReplyAction="http://tempuri.org/ISingleSignOnService/LoginTokenResponse")]
        Bkav.eGovCloud.SingleSignOnService.SsoToken LoginToken(string token, string domain, bool remember);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnService/Logout", ReplyAction="http://tempuri.org/ISingleSignOnService/LogoutResponse")]
        bool Logout(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnService/GetAllDomainIsAuthenticated", ReplyAction="http://tempuri.org/ISingleSignOnService/GetAllDomainIsAuthenticatedResponse")]
        string[] GetAllDomainIsAuthenticated(string domain, string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISingleSignOnServiceChannel : Bkav.eGovCloud.SingleSignOnService.ISingleSignOnService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SingleSignOnServiceClient : System.ServiceModel.ClientBase<Bkav.eGovCloud.SingleSignOnService.ISingleSignOnService>, Bkav.eGovCloud.SingleSignOnService.ISingleSignOnService {
        
        public SingleSignOnServiceClient() {
        }
        
        public SingleSignOnServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SingleSignOnServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingleSignOnServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingleSignOnServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Bkav.eGovCloud.SingleSignOnService.SsoToken RequestToken() {
            return base.Channel.RequestToken();
        }
        
        public Bkav.eGovCloud.SingleSignOnService.SsoToken LoginToken(string token, string domain, bool remember) {
            return base.Channel.LoginToken(token, domain, remember);
        }
        
        public bool Logout(string userName) {
            return base.Channel.Logout(userName);
        }
        
        public string[] GetAllDomainIsAuthenticated(string domain, string userName) {
            return base.Channel.GetAllDomainIsAuthenticated(domain, userName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SingleSignOnService.ISingleSignOnPartnerService")]
    public interface ISingleSignOnPartnerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnPartnerService/ValidateToken", ReplyAction="http://tempuri.org/ISingleSignOnPartnerService/ValidateTokenResponse")]
        Bkav.eGovCloud.SingleSignOnService.SsoUser ValidateToken(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingleSignOnPartnerService/WriteTokenIsAuthenticated", ReplyAction="http://tempuri.org/ISingleSignOnPartnerService/WriteTokenIsAuthenticatedResponse")]
        string WriteTokenIsAuthenticated(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISingleSignOnPartnerServiceChannel : Bkav.eGovCloud.SingleSignOnService.ISingleSignOnPartnerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SingleSignOnPartnerServiceClient : System.ServiceModel.ClientBase<Bkav.eGovCloud.SingleSignOnService.ISingleSignOnPartnerService>, Bkav.eGovCloud.SingleSignOnService.ISingleSignOnPartnerService {
        
        public SingleSignOnPartnerServiceClient() {
        }
        
        public SingleSignOnPartnerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SingleSignOnPartnerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingleSignOnPartnerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingleSignOnPartnerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Bkav.eGovCloud.SingleSignOnService.SsoUser ValidateToken(string token) {
            return base.Channel.ValidateToken(token);
        }
        
        public string WriteTokenIsAuthenticated(string userName) {
            return base.Channel.WriteTokenIsAuthenticated(userName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SingleSignOnService.ICustomerService")]
    public interface ICustomerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/ChangePassword", ReplyAction="http://tempuri.org/ICustomerService/ChangePasswordResponse")]
        Bkav.eGovCloud.SingleSignOnService.ChangePasswordStatus ChangePassword(string userName, string currentPassword, string newPassword, bool isCheckPasswordHistory, int historyCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/ResetPassword", ReplyAction="http://tempuri.org/ICustomerService/ResetPasswordResponse")]
        Bkav.eGovCloud.SingleSignOnService.ChangePasswordStatus ResetPassword(string userName, string newPassword, bool isCheckPasswordHistory, int historyCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetConnection", ReplyAction="http://tempuri.org/ICustomerService/GetConnectionResponse")]
        string GetConnection(string domainName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/GetConnectionByUser", ReplyAction="http://tempuri.org/ICustomerService/GetConnectionByUserResponse")]
        string GetConnectionByUser(string userName, string domainName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/Login", ReplyAction="http://tempuri.org/ICustomerService/LoginResponse")]
        Bkav.eGovCloud.SingleSignOnService.LoginStatus Login(string userName, string password, bool isExpirePassword, int maxPasswordAge, int warningTime, bool isLockoutAccount, int maxLoginFailure, int lockoutDuration);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/LoginNew", ReplyAction="http://tempuri.org/ICustomerService/LoginNewResponse")]
        Bkav.eGovCloud.SingleSignOnService.LoginStatus LoginNew(string userName, string password, bool isExpirePassword, int maxPasswordAge, int warningTime, bool isLockoutAccount, int maxLoginFailure, int lockoutDuration);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/CreateUser", ReplyAction="http://tempuri.org/ICustomerService/CreateUserResponse")]
        Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus CreateUser(string userName, string password, string fullname, bool gender, string phone, string fax, string address, string openid, string domain);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/CreateUserNew", ReplyAction="http://tempuri.org/ICustomerService/CreateUserNewResponse")]
        Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus CreateUserNew(string userName, string userNameAndDomain, string password, string fullname, bool gender, string phone, string fax, string address, string openid, string domain);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/UpdateUser", ReplyAction="http://tempuri.org/ICustomerService/UpdateUserResponse")]
        Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus UpdateUser(string userName, string fullname, bool gender, string phone, string fax, string address, string openid, bool isActivated, string domain);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : Bkav.eGovCloud.SingleSignOnService.ICustomerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<Bkav.eGovCloud.SingleSignOnService.ICustomerService>, Bkav.eGovCloud.SingleSignOnService.ICustomerService {
        
        public CustomerServiceClient() {
        }
        
        public CustomerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Bkav.eGovCloud.SingleSignOnService.ChangePasswordStatus ChangePassword(string userName, string currentPassword, string newPassword, bool isCheckPasswordHistory, int historyCount) {
            return base.Channel.ChangePassword(userName, currentPassword, newPassword, isCheckPasswordHistory, historyCount);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.ChangePasswordStatus ResetPassword(string userName, string newPassword, bool isCheckPasswordHistory, int historyCount) {
            return base.Channel.ResetPassword(userName, newPassword, isCheckPasswordHistory, historyCount);
        }
        
        public string GetConnection(string domainName) {
            return base.Channel.GetConnection(domainName);
        }
        
        public string GetConnectionByUser(string userName, string domainName) {
            return base.Channel.GetConnectionByUser(userName, domainName);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.LoginStatus Login(string userName, string password, bool isExpirePassword, int maxPasswordAge, int warningTime, bool isLockoutAccount, int maxLoginFailure, int lockoutDuration) {
            return base.Channel.Login(userName, password, isExpirePassword, maxPasswordAge, warningTime, isLockoutAccount, maxLoginFailure, lockoutDuration);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.LoginStatus LoginNew(string userName, string password, bool isExpirePassword, int maxPasswordAge, int warningTime, bool isLockoutAccount, int maxLoginFailure, int lockoutDuration) {
            return base.Channel.LoginNew(userName, password, isExpirePassword, maxPasswordAge, warningTime, isLockoutAccount, maxLoginFailure, lockoutDuration);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus CreateUser(string userName, string password, string fullname, bool gender, string phone, string fax, string address, string openid, string domain) {
            return base.Channel.CreateUser(userName, password, fullname, gender, phone, fax, address, openid, domain);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus CreateUserNew(string userName, string userNameAndDomain, string password, string fullname, bool gender, string phone, string fax, string address, string openid, string domain) {
            return base.Channel.CreateUserNew(userName, userNameAndDomain, password, fullname, gender, phone, fax, address, openid, domain);
        }
        
        public Bkav.eGovCloud.SingleSignOnService.UpdateUserStatus UpdateUser(string userName, string fullname, bool gender, string phone, string fax, string address, string openid, bool isActivated, string domain) {
            return base.Channel.UpdateUser(userName, fullname, gender, phone, fax, address, openid, isActivated, domain);
        }
    }
}
