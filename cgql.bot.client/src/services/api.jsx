import axios from 'axios';

class Api {
    constructor() {
        this._api = axios.create({
            withCredentials: false,
            baseURL: 'http://81.70.161.76/'
        });
    }

    axios() {
        return this._api;
    }

    _getDto(err) {
        if (!Object.hasOwn(err, 'response')) {
            return {
                meta: {
                    status: 101,
                    message: 'Connection error, try again later'
                },
                data: {
                    name: err.name,
                    message: err.message
                }
            };
        }
        const response = err.response;
        const ret = { meta: { status: response.status ?? 66, message: err.message }, data: null };

        // no data
        if (!response.data || response.data == '') {
            return ret;
        }

        const data = err.response.data;
        if (Object.hasOwn(data, 'meta')) {
            return data;
        } else {
            return {
                meta: {
                    status: data.status,
                    message: data.title
                },
                data: null
            };
        }
    }

    // single post
    async _post(url, body) {
        return await this._api.post(url, body).then(res => {
            // 200 must be our custom data
            return res.data;
        }).catch(err => {
            return this._getDto(err);
        });
    }

    // post with retry
    async post(url, body) {
        return await this._post(url, body);
    }

    async _get(url, params) {
        return await this._api.get(url, { params: params }).then(res => {
            // 200 must be our custom data
            return res.data;
        }).catch(err => {
            return this._getDto(err);
        });
    }

    async get(url, params) {
        return await this._get(url, params);
    }
}

var api = new Api();

export default api;