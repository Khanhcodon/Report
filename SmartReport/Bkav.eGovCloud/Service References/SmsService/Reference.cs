﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bkav.eGovCloud.SmsService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SmsService.SMSServiceSoap")]
    public interface SMSServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Send", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Send(string Content, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendWarning", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendWarning(string content, string accounts, string groups, int attachSMSId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendWithServiceType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendWithServiceType(string Content, int serviceType, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendWithPriority", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendWithPriority(string Content, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendWithPriorityByType", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendWithPriorityByType(int serviceTypeId, string Content, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendWithPriorityByTypeBNI", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendWithPriorityByTypeBNI(int serviceTypeId, string Content, int attachSMSId, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendSchedule", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendSchedule(string content, string account, System.DateTime timeSend);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendReturnID", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string SendReturnID(string Content, string tonumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetResult", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Bkav.eGovCloud.SmsService.Result GetResult(long sms_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaticsSpecialSim", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string StaticsSpecialSim(System.DateTime startTime, System.DateTime endTime, int serviceTypeId);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class Result : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int isSentField;
        
        private int retryNumberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int isSent {
            get {
                return this.isSentField;
            }
            set {
                this.isSentField = value;
                this.RaisePropertyChanged("isSent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int retryNumber {
            get {
                return this.retryNumberField;
            }
            set {
                this.retryNumberField = value;
                this.RaisePropertyChanged("retryNumber");
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
    public interface SMSServiceSoapChannel : Bkav.eGovCloud.SmsService.SMSServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SMSServiceSoapClient : System.ServiceModel.ClientBase<Bkav.eGovCloud.SmsService.SMSServiceSoap>, Bkav.eGovCloud.SmsService.SMSServiceSoap {
        
        public SMSServiceSoapClient() {
        }
        
        public SMSServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SMSServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Send(string Content, string tonumber) {
            return base.Channel.Send(Content, tonumber);
        }
        
        public string SendWarning(string content, string accounts, string groups, int attachSMSId) {
            return base.Channel.SendWarning(content, accounts, groups, attachSMSId);
        }
        
        public string SendWithServiceType(string Content, int serviceType, string tonumber) {
            return base.Channel.SendWithServiceType(Content, serviceType, tonumber);
        }
        
        public string SendWithPriority(string Content, string tonumber) {
            return base.Channel.SendWithPriority(Content, tonumber);
        }
        
        public string SendWithPriorityByType(int serviceTypeId, string Content, string tonumber) {
            return base.Channel.SendWithPriorityByType(serviceTypeId, Content, tonumber);
        }
        
        public string SendWithPriorityByTypeBNI(int serviceTypeId, string Content, int attachSMSId, string tonumber) {
            return base.Channel.SendWithPriorityByTypeBNI(serviceTypeId, Content, attachSMSId, tonumber);
        }
        
        public string SendSchedule(string content, string account, System.DateTime timeSend) {
            return base.Channel.SendSchedule(content, account, timeSend);
        }
        
        public string SendReturnID(string Content, string tonumber) {
            return base.Channel.SendReturnID(Content, tonumber);
        }
        
        public Bkav.eGovCloud.SmsService.Result GetResult(long sms_id) {
            return base.Channel.GetResult(sms_id);
        }
        
        public string StaticsSpecialSim(System.DateTime startTime, System.DateTime endTime, int serviceTypeId) {
            return base.Channel.StaticsSpecialSim(startTime, endTime, serviceTypeId);
        }
    }
}
