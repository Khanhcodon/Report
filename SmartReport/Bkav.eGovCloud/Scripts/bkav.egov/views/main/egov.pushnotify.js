var pushNotify = function () {
    this.init();
}


pushNotify.prototype.init = function () {
    /// <summary>
    /// Khởi tạo đối tượng pushNotify
    /// </summary>
    var that = this;
    window.addEventListener('load', function () {
        // Check that service workers are supported, if so, progressively  
        // enhance and add push messaging support, otherwise continue without it.  
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/Scripts/bkav.egov/libs/service-worker.js')
            .then(that.initialiseState);
        } else {
            console.warn('Service workers aren\'t supported in this browser.');
        }
    });
}
pushNotify.prototype.changeStatus = function (enable) {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enable"></param>
    if (!enable) {
        this.unsubscribe();
    } else {
        this.subscribe();
    }
}

pushNotify.prototype.initialiseState = function () {
    // Are Notifications supported in the service worker?  
    if (!('showNotification' in ServiceWorkerRegistration.prototype)) {
        console.warn('Notifications aren\'t supported.');
        return;
    }

    // Check the current Notification permission.  
    // If its denied, it's a permanent block until the  
    // user changes the permission  
    if (Notification.permission === 'denied') {
        console.warn('The user has blocked notifications.');
        return;
    }

    // Check if push messaging is supported  
    if (!('PushManager' in window)) {
        console.warn('Push messaging isn\'t supported.');
        return;
    }

    // We need the service worker registration to check for a subscription  
    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        // Do we already have a push message subscription?  
        serviceWorkerRegistration.pushManager.getSubscription()
          .then(function (subscription) {
              // Enable any UI which subscribes / unsubscribes from  
              // push messages.  
              if (!subscription) {
                  // We aren't subscribed to push, so set UI  
                  // to allow the user to enable push  
                  return;
              }

              // Keep your server in sync with the latest subscriptionId
              sendSubscriptionToServer(subscription);

              // Set your UI to show they have subscribed for  
              // push messages  
              pushButton.textContent = 'Disable Push Messages';
              isPushEnabled = true;
          })
          .catch(function (err) {
              console.warn('Error during getSubscription()', err);
          });
    });
}
pushNotify.prototype.unsubscribe = function () {
    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.getSubscription().then(
            function (pushSubscription) {
                if (!pushSubscription) {
                    return;
                }
                var subscriptionId = pushSubscription.subscriptionId;
                pushSubscription.unsubscribe().then(function (successful) {
                }).catch(function (e) {
                    console.error('Error thrown while unsbscribing from push messaging.', e);
                });
            });
    });
}
pushNotify.prototype.subscribe = function () {
    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        serviceWorkerRegistration.pushManager.subscribe()
          .then(function (subscription) {
              // The subscription was successful  
              // TODO: Send the subscription.endpoint to your server  
              // and save it to send a push message at a later date
              return sendSubscriptionToServer(subscription);
          })
          .catch(function (e) {
              if (Notification.permission === 'denied') {
                  // The user denied the notification permission which  
                  // means we failed to subscribe and the user will need  
                  // to manually change the notification permission to  
                  // subscribe to push messages  
                  console.warn('Permission for Notifications was denied');
              } else {
                  // A problem occurred with the subscription; common reasons  
                  // include network errors, and lacking gcm_sender_id and/or  
                  // gcm_user_visible_only in the manifest.  
                  console.error('Unable to subscribe to push.', e);
              }
          });
    });
}
// send subscription id to server
var sendSubscriptionToServer = function (subscription) {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="subscription"></param>
    console.log(subscription);
}
window.pushNotify = new pushNotify();