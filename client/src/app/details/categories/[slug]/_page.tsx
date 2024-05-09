import axios, {AxiosResponse} from 'axios';

export const BASE_URL = "/api/event-service"

export const fetchEventsById = async (id:string  ) => {
	let response : AxiosResponse;
	try {
		response =  await axios.get(`${BASE_URL}/Events/id/${id}`)
		return response.data;
	} catch (error) {
		return null;
	}
};