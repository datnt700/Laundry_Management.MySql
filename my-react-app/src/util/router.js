import requestURL from "./requestURL" 

const END_POINT = {
    LIST: "GetList",
    LOGIN: "login",
}

const getLoginAPI = () => {
    return requestURL.post(`${END_POINT.LOGIN}`)
}

export default getLoginAPI;