
(function () {

    var _LOADMORECLASS = 'sl-load-more';   

    var ScrollLoadMore = function (selector, options) {
        this.options = Object.assign({
            offset: 100,   // Vùng bao xác định scroll thuộc bottom hoặc top tính theo px
            fireDelay: 0,   // Thời gian delay trước khi gọi callback, đặt 0 để bỏ delay.
            direction: 'next' // hướng scroll to load.
        }, options || {});

        this.$container = typeof selector === 'string' ? document.querySelector(selector) : selector;

        this.callback = this.options.callback;

        if (!this.$container || this.$container.hasScrollLoadMore) {
            return;
        }

        this.$container.hasScrollLoadMore = true;

        this._init();
    };

    ScrollLoadMore.prototype._init = function () {
        this.containerBottom = this.$container.getBoundingClientRect().bottom;
        this.containerTop = this.$container.getBoundingClientRect().top;

        this.lastScrollTop = this.$container.scrollTop;
        this.$loadMoreElement = this._ensureLoadmoreElement();

        this.onScrolling = false;
        this.rafId = null;
        this.timer = null;

        this.onFiring = false; 			// Đang chạy callback

        this.$container.addEventListener('scroll', this._scrollHandler.bind(this));
    }

    ScrollLoadMore.prototype._scrollHandler = function () {
        this.timer && clearTimeout(this.timer);
        this.onScrolling = true;

        !this.rafId && this._startChecking();

        this.timer = window.setTimeout(function () {
            this.onScrolling = false;
            this._stopChecking();
        }.bind(this), 250);
    }

    ScrollLoadMore.prototype._scrollFire = function () {
        if (this._hasFire()) {
            this._stopChecking();
            this._callback();
            return;
        }

        this.onFiring = false;
        this.rafId = requestAnimationFrame(this._scrollFire.bind(this));
    }

    ScrollLoadMore.prototype._hasFire = function () {
        var bound = this.$loadMoreElement.getBoundingClientRect();
        return this.options.direction === 'next' ? (bound.bottom <= this.containerBottom + this.options.offset)
					: (bound.bottom >= this.containerTop - this.options.offset);
    }

    ScrollLoadMore.prototype._callback = function () {
        if (this.onFiring || typeof this.callback !== 'function') return;

        (this.onFiring = true) && setTimeout(function () { this.callback(); }.bind(this), this.options.fireDelay);
    }

    ScrollLoadMore.prototype._ensureLoadmoreElement = function () {
        var loadmoreElement = this.$container.getElementsByClassName(_LOADMORECLASS), result;
        if (loadmoreElement && loadmoreElement.length > 0) return loadmoreElement[0];

        result = document.createElement("div");
        result.classList.add(_LOADMORECLASS);
        result.style.height = "1px";
        result.style.width = "100%";
        result.style.background = "transparent";
        result.style.border = "none";

        this.options.direction === 'next' ? this.$container.append(result) : this.$container.prepend(result);
        return result;
    }

    ScrollLoadMore.prototype._startChecking = function () {
        this.onScrolling && (this.rafId = requestAnimationFrame(this._scrollFire.bind(this)));
    }

    ScrollLoadMore.prototype._stopChecking = function () {
        this.rafId && cancelAnimationFrame(this.rafId);
        this.rafId = null;
    }

    window.ScrollLoadMore = ScrollLoadMore;
})();