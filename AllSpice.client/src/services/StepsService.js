import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class StepsService {
  async getSteps(id) {
    const res = await api.get("api/recipes/" + id + "/steps")
    logger.log("steps new", res.data)
    AppState.steps = res.data
  }
  async addStep(body) {
    const res = await api.post("api/steps", body)
    logger.log(res.data)
    AppState.steps = [...AppState.steps, res.data]

  }
}
export const stepsService = new StepsService();