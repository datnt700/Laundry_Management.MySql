import instance from "./requestURL";

const autAPI ={
    login : (phone,password) => {
        const url ='Auth/login';
        return instance.post(url,{phone,password});
    },

    register : (username,phone,password) => {
        const url ='Auth/register';
        return instance.post(url,{username,phone,password});
    },

    machine : () => {
        const url = 'Machine/GetList';
        return instance.get(url);
    }
}

export default autAPI;