import { LoginParams } from './login-params';

export interface RegisterOrganisationParams extends LoginParams {
  organisationName: string
}
