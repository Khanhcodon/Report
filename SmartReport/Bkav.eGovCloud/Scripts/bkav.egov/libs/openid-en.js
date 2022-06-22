/*
	Simple OpenID Plugin
	http://code.google.com/p/openid-selector/
	
	This code is licensed under the New BSD License.
*/

var providers_large = {
	google : {
		name : 'Google',
		url : 'https://www.google.com/accounts/o8/id'
	},
	yahoo : {
		name : 'Yahoo',
		url : 'http://me.yahoo.com/'
	},
	aol : {
		name : 'AOL',
		label : 'Nhập screenname AOL của bạn.',
		url : 'http://openid.aol.com/{username}'
	},
	myopenid : {
		name : 'MyOpenID',
		label : 'Nhập tên đăng nhập MyOpenID của bạn.',
		url : 'http://{username}.myopenid.com/'
	},
	openid : {
		name : 'OpenID',
		label : 'Nhập OpenID của bạn.',
		url : null
	}
};

var providers_small = {
	livejournal : {
		name : 'LiveJournal',
		label: 'Nhập tên đăng nhập Livejournal của bạn.',
		url : 'http://{username}.livejournal.com/'
	},
	/* flickr: {
		name: 'Flickr',        
		label: 'Enter your Flickr username.',
		url: 'http://flickr.com/{username}/'
	}, */
	/* technorati: {
		name: 'Technorati',
		label: 'Enter your Technorati username.',
		url: 'http://technorati.com/people/technorati/{username}/'
	}, */
	wordpress : {
		name : 'Wordpress',
		label : 'Nhập tên đăng nhập Wordpress.com của bạn.',
		url : 'http://{username}.wordpress.com/'
	},
	blogger : {
		name : 'Blogger',
		label : 'Nhập tên đăng nhập Blogger của bạn',
		url : 'http://{username}.blogspot.com/'
	},
	verisign : {
		name : 'Verisign',
		label : 'Nhập tên đăng nhập Verisign của bạn',
		url : 'http://{username}.pip.verisignlabs.com/'
	},
	/* vidoop: {
		name: 'Vidoop',
		label: 'Your Vidoop username',
		url: 'http://{username}.myvidoop.com/'
	}, */
	/* launchpad: {
		name: 'Launchpad',
		label: 'Your Launchpad username',
		url: 'https://launchpad.net/~{username}'
	}, */
	claimid : {
		name : 'ClaimID',
		label: 'Nhập tên đăng nhập ClaimID của bạn',
		url : 'http://claimid.com/{username}'
	},
	clickpass : {
		name : 'ClickPass',
		label: 'Nhập tên đăng nhập ClickPass của bạn',
		url : 'http://clickpass.com/public/{username}'
	},
	google_profile : {
		name : 'Google Profile',
		label: 'Nhập tên đăng nhập Google Profile của bạn',
		url : 'http://www.google.com/profiles/{username}'
	}
};