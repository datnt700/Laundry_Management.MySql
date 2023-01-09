import axiosClient from "./axiosClient";

const END_POINT = {
    LIST: "GetList",
    LOGIN: "Login",
}

export const getAPI = () => {
    return axiosClient.get(`${END_POINT.LIST}`)
}

