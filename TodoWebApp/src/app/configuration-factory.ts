import {configurationParameters} from '../environments/environment';
import {Configuration} from '../todoApiClient';
import {GrantService} from './grant.service';

export function ConfigurationFactory(grantService: GrantService) {
  const config = new Configuration(configurationParameters);
  config.apiKeys = {Authorization: grantService.getAccessToken()};
  return config;
}
